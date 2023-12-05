using System.Dynamic;
using Elastic.Apm.Api;
using ETLBox;
using ETLBox.DataFlow;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nox.Integration.Abstractions;
using Nox.Integration.Abstractions.Adapters;
using Nox.Integration.Constants;
using Nox.Integration.Exceptions;
using Nox.Integration.Extensions.Send;
using Nox.Solution;

namespace Nox.Integration.Services;

internal sealed class NoxIntegration: INoxIntegration
{
    private readonly ILogger _logger;
    private readonly INoxIntegrationDbContextFactory _dbContextFactory;
    private IntegrationMergeStates? _mergeStates;
    
    public string Name { get; }
    public string? Description { get; }
    public IntegrationSchedule? Schedule { get; }
    public IntegrationMergeType MergeType { get; }
    
    public IntegrationTransformType TransformType { get; }
    public INoxReceiveAdapter? ReceiveAdapter { get; set; }
    public INoxSendAdapter? SendAdapter { get; set; }
    public List<string>? TargetIdColumns { get; private set; } = null;
    public List<string>? TargetDateColumns { get; private set; } = null;

    public List<string>? SourceFilterColumns { get; set; }

    public NoxIntegration(ILogger logger, Solution.Integration definition, INoxIntegrationDbContextFactory dbContextFactory)
    {
        _logger = logger;
        _dbContextFactory = dbContextFactory;
        Name = definition.Name;
        Schedule = definition.Schedule;
        Description = definition.Description;
        MergeType = definition.MergeType;
        TransformType = definition.TransformationType;
        AddSourceFilterColumns(definition.Source);
        AddTargetWatermark(definition.Target);
    }
    
    public async Task ExecuteAsync(ITransaction apmTransaction, INoxCustomTransformHandler? handler = null)
    {
        _mergeStates = await GetMergeStates();
        if (SourceFilterColumns != null && SourceFilterColumns.Any())
        {
            ReceiveAdapter!.ApplyFilter(SourceFilterColumns, _mergeStates);    
        }
        
        switch (MergeType)
        {
            case IntegrationMergeType.MergeNew:
                await apmTransaction.CaptureSpan("MergeNew", ApiConstants.ActionExec, async () => await ExecuteMergeNew(handler));
                break;
            case IntegrationMergeType.AddNew:
                await apmTransaction.CaptureSpan("AddNew", ApiConstants.ActionExec, async() => await ExecuteAddNew(handler));
                break;
        }
        await SetLastMergeStates();
    }

    private async Task ExecuteMergeNew(INoxCustomTransformHandler? handler)
    {
        var source = ReceiveAdapter!.DataFlowSource;
        

        IDataFlowSource<ExpandoObject>? transformSource = null;
        
        if (TransformType == IntegrationTransformType.CustomTransform)
        {
            if (handler == null) throw new NoxIntegrationException("Cannot execute custom transform, handler not registered.");
            var rowTransform = new RowTransformation<ExpandoObject, ExpandoObject>(sourceRecord => handler.Invoke(sourceRecord));
            transformSource =  source.LinkTo(rowTransform);
        }
        
        CustomDestination? postProcessDestination;
        switch (SendAdapter!.AdapterType)
        {
            case IntegrationTargetAdapterType.DatabaseTable:
                if (transformSource != null)
                {
                    postProcessDestination = transformSource.LinkToDatabaseTable((INoxDatabaseSendAdapter)SendAdapter, TargetIdColumns, TargetDateColumns);    
                }
                else
                {
                    postProcessDestination = source.LinkToDatabaseTable((INoxDatabaseSendAdapter)SendAdapter, TargetIdColumns, TargetDateColumns);    
                }
                
                break;
            default:
                throw new NotImplementedException($"Send adapter type: {Enum.GetName(SendAdapter!.AdapterType)} has not been implemented!");
        }

        var unChanged = 0;
        var inserts = 0;
        var updates = 0;

        postProcessDestination.WriteAction = (row, _) =>
        {
            var record = (IDictionary<string, object?>)row;
            if ((ChangeAction)record["ChangeAction"]! == ChangeAction.Insert)
            {
                inserts++;
                //Fire events
                //if(entityCreatedMsg is not null) SendChangeEvent(loader, row, entityCreatedMsg, NoxEventSource.EtlMerge);
                UpdateMergeStates(record);
            }
            else if ((ChangeAction)record["ChangeAction"]! == ChangeAction.Update)
            {
                updates++;
                //Fire events
                //if (entityUpdatedMsg is not null) SendChangeEvent(loader, row, entityUpdatedMsg, NoxEventSource.EtlMerge);
                UpdateMergeStates(record);
            }
            else if ((ChangeAction)record["ChangeAction"]! == ChangeAction.Exists)
            {
                unChanged++;
            }
        };

        try
        {
            await Network.ExecuteAsync(source);
        }
        catch (Exception ex)
        {
            _logger.LogCritical("Failed to run Merge for integration: {integrationName}", Name);
            _logger.LogError("{message}", ex.Message);
            throw;
        }  

        //Log analytics
    }

    private Task ExecuteAddNew(INoxCustomTransformHandler? handler)
    {
        return Task.CompletedTask;
    }

    private void AddSourceFilterColumns(IntegrationSource source)
    {
        if (source.Watermark == null) return;
        var watermark = source.Watermark;
        if (watermark.DateColumns != null && watermark.DateColumns.Any())
        {
            SourceFilterColumns = new List<string>();
            foreach (var filterColumn in watermark.DateColumns)
            {
                SourceFilterColumns.Add(filterColumn);
            }
        }
    }

    private void AddTargetWatermark(IntegrationTarget target)
    {
        if (target.TableOptions?.Watermark == null) return;
        
        var watermark = target.TableOptions.Watermark;
        if (watermark.SequentialKeyColumns != null && watermark.SequentialKeyColumns.Any())
        {
            TargetIdColumns = new List<string>();
            foreach (var keyColumn in watermark.SequentialKeyColumns)
            {
                TargetIdColumns.Add(keyColumn);
            }
        }
        if (watermark.DateColumns != null && watermark.DateColumns.Any())
        {
            TargetDateColumns = new List<string>();
            foreach (var dateColumn in watermark.DateColumns)
            {
                TargetDateColumns.Add(dateColumn);
            }
        }
    }

    private async Task<IntegrationMergeStates> GetMergeStates()
    {
        var result = new IntegrationMergeStates();
        var dbContext = _dbContextFactory.CreateContext();
        
        var addedFilterColumn = false;

        if (SourceFilterColumns != null && SourceFilterColumns.Any())
        {
            addedFilterColumn = true;
            
            foreach(var filterColumn in SourceFilterColumns)
            {
                var lastMergeTimestamp = await GetLastMergeTimestamp(dbContext, filterColumn);
                result[filterColumn] = new IntegrationMergeState
                {
                    Integration = Name,
                    Property = filterColumn,
                    LastDateLoadedUtc = lastMergeTimestamp
                };
            }   
        }

        if (!addedFilterColumn)
        {
            var lastMergeTimestamp = await GetLastMergeTimestamp(dbContext, IntegrationContextConstants.DefaultFilterProperty);
            result[IntegrationContextConstants.DefaultFilterProperty] = new IntegrationMergeState
            {
                Integration = Name,
                Property = IntegrationContextConstants.DefaultFilterProperty,
                LastDateLoadedUtc = lastMergeTimestamp
            };
            await RemoveNonDefaultMergeTimestamps(dbContext);
        }
        else
        {
            await RemoveDefaultMergeTimestamp(dbContext);
        }

        await dbContext.SaveChangesAsync();
        return result;
    }

    private async Task<DateTime> GetLastMergeTimestamp(NoxIntegrationDbContext dbContext, string filterName)
    {
        var result = IntegrationContextConstants.MinSqlMergeDate;
        var timestamp = await dbContext.MergeStates!.FirstOrDefaultAsync(ts =>
            ts.Integration!.Equals(Name) &&
            ts.Property!.Equals(filterName));
        if (timestamp != null)
        {
            result = timestamp.LastDateLoadedUtc;
        }
        else
        {
            timestamp = new IntegrationMergeState
            {
                Integration = Name,
                Property = filterName,
                LastDateLoadedUtc = result,
                IsUpdated = true
            };
            await dbContext.MergeStates!.AddAsync(timestamp);
        }
        return result;
    }

    private async Task RemoveNonDefaultMergeTimestamps(NoxIntegrationDbContext dbContext)
    {
        var timestamp = await dbContext.MergeStates!.FirstOrDefaultAsync(ts =>
            ts.Integration!.Equals(Name) &&
            !ts.Property!.Equals(IntegrationContextConstants.DefaultFilterProperty));
        if (timestamp != null) dbContext.MergeStates!.Remove(timestamp);
    }

    private async Task RemoveDefaultMergeTimestamp(NoxIntegrationDbContext dbContext)
    {
        var timestamps = await dbContext.MergeStates!.Where(ts =>
            ts.Integration!.Equals(Name) &&
            !ts.Property!.Equals(IntegrationContextConstants.DefaultFilterProperty)).ToListAsync();
        if (timestamps.Any())
        {
            dbContext.MergeStates!.RemoveRange(timestamps);
        }
    }

    private void UpdateMergeStates(IDictionary<string, object?> record)
    {
        if (_mergeStates == null) return;
        foreach (var filterColumn in _mergeStates.Keys)
        {
            if (record.TryGetValue(filterColumn, out var filterColumnValue))
            {
                if (filterColumnValue == null) continue;
                
                if (DateTime.TryParse(filterColumnValue.ToString(), out var fieldValue))
                {
                    if (fieldValue > _mergeStates[filterColumn].LastDateLoadedUtc)
                    {
                        var changeEntry = _mergeStates[filterColumn];
                        changeEntry.LastDateLoadedUtc = fieldValue;
                        changeEntry.IsUpdated = true;
                        _mergeStates[filterColumn] = changeEntry;
                    }
                }
            }
            else
            {
                if (record.TryGetValue("ChangeDate", out var changeDate))
                {
                    if (changeDate == null) continue;
                    
                    if (DateTime.TryParse(changeDate.ToString(), out var changeDateValue))
                    {
                        var changeEntry = _mergeStates[IntegrationContextConstants.DefaultFilterProperty];
                        changeEntry.LastDateLoadedUtc = changeDateValue.ToUniversalTime();
                        changeEntry.IsUpdated = true;
                        _mergeStates[IntegrationContextConstants.DefaultFilterProperty] = changeEntry;    
                    }
                }
            }
        }
    }

    private async Task SetLastMergeStates()
    {
        if (_mergeStates == null) return;
        
        var dbContext = _dbContextFactory.CreateContext();
        foreach (var (filterColumn, mergeState) in _mergeStates)
        {
            if (mergeState.IsUpdated)
            {
                await SetLastMergeState(dbContext, filterColumn, mergeState.LastDateLoadedUtc);
            }
        }

        await dbContext.SaveChangesAsync();
    }

    private async Task SetLastMergeState(NoxIntegrationDbContext dbContext, string propertyName, DateTime lastMergeDateTime)
    {
        var timestamp = await dbContext.MergeStates!.SingleAsync(ts =>
            ts.Integration!.Equals(Name) &&
            ts.Property!.Equals(propertyName));
        timestamp.LastDateLoadedUtc = lastMergeDateTime;
    }
}