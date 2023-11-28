﻿// Generated

#nullable enable

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System;
using System.Net.Http.Headers;
using Nox.Application;
using Nox.Application.Dto;
using Nox.Extensions;
using Cryptocash.Application;
using Cryptocash.Application.Dto;
using Cryptocash.Application.Queries;
using Cryptocash.Application.Commands;
using Cryptocash.Domain;
using Cryptocash.Infrastructure.Persistence;
using Nox.Types;

namespace Cryptocash.Presentation.Api.OData;

public abstract partial class CountriesControllerBase : ODataController
{
    
    #region Owned Relationships
    
    [EnableQuery]
    public virtual async Task<ActionResult<IQueryable<CountryTimeZoneDto>>> GetCountryTimeZones([FromRoute] System.String key)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        var item = (await _mediator.Send(new GetCountryByIdQuery(key))).SingleOrDefault();
        
        if (item is null)
        {
            return NotFound();
        }
        
        return Ok(item.CountryTimeZones);
    }
    
    [EnableQuery]
    [HttpGet("/api/Countries/{key}/CountryTimeZones/{relatedKey}")]
    public virtual async Task<ActionResult<CountryTimeZoneDto>> GetCountryTimeZonesNonConventional(System.String key, System.Int64 relatedKey)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        var child = await TryGetCountryTimeZones(key, new CountryTimeZoneKeyDto(relatedKey));
        if (child == null)
        {
            return NotFound();
        }
        
        return Ok(child);
    }
    
    public virtual async Task<ActionResult> PostToCountryTimeZones([FromRoute] System.String key, [FromBody] CountryTimeZoneUpsertDto countryTimeZone)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var etag = Request.GetDecodedEtagHeader();
        var createdKey = await _mediator.Send(new CreateCountryTimeZonesForCountryCommand(new CountryKeyDto(key), countryTimeZone, etag));
        if (createdKey == null)
        {
            return NotFound();
        }
        
        var child = await TryGetCountryTimeZones(key, createdKey);
        if (child == null)
        {
            return NotFound();
        }
        
        return Created(child);
    }
    
    public virtual async Task<ActionResult<CountryTimeZoneDto>> PutToCountryTimeZones(System.String key, [FromBody] CountryTimeZoneUpsertDto countryTimeZone)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var etag = Request.GetDecodedEtagHeader();
        var updatedKey = await _mediator.Send(new UpdateCountryTimeZonesForCountryCommand(new CountryKeyDto(key), countryTimeZone, etag));
        if (updatedKey == null)
        {
            return NotFound();
        }
        
        var child = await TryGetCountryTimeZones(key, updatedKey);
        if (child == null)
        {
            return NotFound();
        }
        
        return Ok(child);
    }
    
    public virtual async Task<ActionResult> PatchToCountryTimeZones(System.String key, [FromBody] Delta<CountryTimeZoneUpsertDto> countryTimeZone)
    {
        if (!ModelState.IsValid || countryTimeZone is null)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        var updateProperties = new Dictionary<string, dynamic>();
        
        foreach (var propertyName in countryTimeZone.GetChangedPropertyNames())
        {
            if(countryTimeZone.TryGetPropertyValue(propertyName, out dynamic value))
            {
                updateProperties[propertyName] = value;                
            }           
        }
        
        if(!updateProperties.ContainsKey("Id"))
        {
            throw new Nox.Exceptions.BadRequestException("Id is required.");
        }
        
        var etag = Request.GetDecodedEtagHeader();
        var updated = await _mediator.Send(new PartialUpdateCountryTimeZonesForCountryCommand(new CountryKeyDto(key), new CountryTimeZoneKeyDto(updateProperties["Id"]), updateProperties, etag));
        
        if (updated is null)
        {
            return NotFound();
        }
        var child = await TryGetCountryTimeZones(key, updated);
        if (child == null)
        {
            return NotFound();
        }
        
        return Ok(child);
    }
    
    [HttpDelete("/api/Countries/{key}/CountryTimeZones/{relatedKey}")]
    public virtual async Task<ActionResult> DeleteCountryTimeZoneNonConventional(System.String key, System.Int64 relatedKey)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        var result = await _mediator.Send(new DeleteCountryTimeZonesForCountryCommand(new CountryKeyDto(key), new CountryTimeZoneKeyDto(relatedKey)));
        if (!result)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    protected async Task<CountryTimeZoneDto?> TryGetCountryTimeZones(System.String key, CountryTimeZoneKeyDto childKeyDto)
    {
        var parent = (await _mediator.Send(new GetCountryByIdQuery(key))).SingleOrDefault();
        return parent?.CountryTimeZones.SingleOrDefault(x => x.Id == childKeyDto.keyId);
    }
    
    [EnableQuery]
    public virtual async Task<ActionResult<IQueryable<HolidayDto>>> GetHolidays([FromRoute] System.String key)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        var item = (await _mediator.Send(new GetCountryByIdQuery(key))).SingleOrDefault();
        
        if (item is null)
        {
            return NotFound();
        }
        
        return Ok(item.Holidays);
    }
    
    [EnableQuery]
    [HttpGet("/api/Countries/{key}/Holidays/{relatedKey}")]
    public virtual async Task<ActionResult<HolidayDto>> GetHolidaysNonConventional(System.String key, System.Int64 relatedKey)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        var child = await TryGetHolidays(key, new HolidayKeyDto(relatedKey));
        if (child == null)
        {
            return NotFound();
        }
        
        return Ok(child);
    }
    
    public virtual async Task<ActionResult> PostToHolidays([FromRoute] System.String key, [FromBody] HolidayUpsertDto holiday)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var etag = Request.GetDecodedEtagHeader();
        var createdKey = await _mediator.Send(new CreateHolidaysForCountryCommand(new CountryKeyDto(key), holiday, etag));
        if (createdKey == null)
        {
            return NotFound();
        }
        
        var child = await TryGetHolidays(key, createdKey);
        if (child == null)
        {
            return NotFound();
        }
        
        return Created(child);
    }
    
    public virtual async Task<ActionResult<HolidayDto>> PutToHolidays(System.String key, [FromBody] HolidayUpsertDto holiday)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var etag = Request.GetDecodedEtagHeader();
        var updatedKey = await _mediator.Send(new UpdateHolidaysForCountryCommand(new CountryKeyDto(key), holiday, etag));
        if (updatedKey == null)
        {
            return NotFound();
        }
        
        var child = await TryGetHolidays(key, updatedKey);
        if (child == null)
        {
            return NotFound();
        }
        
        return Ok(child);
    }
    
    public virtual async Task<ActionResult> PatchToHolidays(System.String key, [FromBody] Delta<HolidayUpsertDto> holiday)
    {
        if (!ModelState.IsValid || holiday is null)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        var updateProperties = new Dictionary<string, dynamic>();
        
        foreach (var propertyName in holiday.GetChangedPropertyNames())
        {
            if(holiday.TryGetPropertyValue(propertyName, out dynamic value))
            {
                updateProperties[propertyName] = value;                
            }           
        }
        
        if(!updateProperties.ContainsKey("Id"))
        {
            throw new Nox.Exceptions.BadRequestException("Id is required.");
        }
        
        var etag = Request.GetDecodedEtagHeader();
        var updated = await _mediator.Send(new PartialUpdateHolidaysForCountryCommand(new CountryKeyDto(key), new HolidayKeyDto(updateProperties["Id"]), updateProperties, etag));
        
        if (updated is null)
        {
            return NotFound();
        }
        var child = await TryGetHolidays(key, updated);
        if (child == null)
        {
            return NotFound();
        }
        
        return Ok(child);
    }
    
    [HttpDelete("/api/Countries/{key}/Holidays/{relatedKey}")]
    public virtual async Task<ActionResult> DeleteHolidayNonConventional(System.String key, System.Int64 relatedKey)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        var result = await _mediator.Send(new DeleteHolidaysForCountryCommand(new CountryKeyDto(key), new HolidayKeyDto(relatedKey)));
        if (!result)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    protected async Task<HolidayDto?> TryGetHolidays(System.String key, HolidayKeyDto childKeyDto)
    {
        var parent = (await _mediator.Send(new GetCountryByIdQuery(key))).SingleOrDefault();
        return parent?.Holidays.SingleOrDefault(x => x.Id == childKeyDto.keyId);
    }
    
    #endregion
    
    
    #region Relationships
    
    public async Task<ActionResult> CreateRefToCurrency([FromRoute] System.String key, [FromRoute] System.String relatedKey)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var createdRef = await _mediator.Send(new CreateRefCountryToCurrencyCommand(new CountryKeyDto(key), new CurrencyKeyDto(relatedKey)));
        if (!createdRef)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    public async Task<ActionResult> GetRefToCurrency([FromRoute] System.String key)
    {
        var related = (await _mediator.Send(new GetCountryByIdQuery(key))).Select(x => x.Currency).SingleOrDefault();
        if (related is null)
        {
            return NotFound();
        }
        
        var references = new System.Uri($"Currencies/{related.Id}", UriKind.Relative);
        return Ok(references);
    }
    
    public virtual async Task<ActionResult> PostToCurrency([FromRoute] System.String key, [FromBody] CurrencyCreateDto currency)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        currency.CountriesId = new List<System.String> { key };
        var createdKey = await _mediator.Send(new CreateCurrencyCommand(currency, _cultureCode));
        
        var createdItem = (await _mediator.Send(new GetCurrencyByIdQuery(createdKey.keyId))).SingleOrDefault();
        
        return Created(createdItem);
    }
    
    [EnableQuery]
    public virtual async Task<SingleResult<CurrencyDto>> GetCurrency(System.String key)
    {
        var related = (await _mediator.Send(new GetCountryByIdQuery(key))).Where(x => x.Currency != null);
        if (!related.Any())
        {
            return SingleResult.Create<CurrencyDto>(Enumerable.Empty<CurrencyDto>().AsQueryable());
        }
        return SingleResult.Create(related.Select(x => x.Currency!));
    }
    
    public virtual async Task<ActionResult<CurrencyDto>> PutToCurrency(System.String key, [FromBody] CurrencyUpdateDto currency)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var related = (await _mediator.Send(new GetCountryByIdQuery(key))).Select(x => x.Currency).SingleOrDefault();
        if (related == null)
        {
            return NotFound();
        }
        
        var etag = Request.GetDecodedEtagHeader();
        var updated = await _mediator.Send(new UpdateCurrencyCommand(related.Id, currency, _cultureCode, etag));
        if (updated == null)
        {
            return NotFound();
        }
        
        return Ok();
    }
    
    public async Task<ActionResult> CreateRefToCommissions([FromRoute] System.String key, [FromRoute] System.Int64 relatedKey)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var createdRef = await _mediator.Send(new CreateRefCountryToCommissionsCommand(new CountryKeyDto(key), new CommissionKeyDto(relatedKey)));
        if (!createdRef)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    [HttpPut("/api/Countries/{key}/Commissions/$ref")]
    public async Task<ActionResult> UpdateRefToCommissionsNonConventional([FromRoute] System.String key, [FromBody] ReferencesDto<System.Int64> referencesDto)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var relatedKeysDto = referencesDto.References.Select(x => new CommissionKeyDto(x)).ToList();
        var updatedRef = await _mediator.Send(new UpdateRefCountryToCommissionsCommand(new CountryKeyDto(key), relatedKeysDto));
        if (!updatedRef)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    public async Task<ActionResult> GetRefToCommissions([FromRoute] System.String key)
    {
        var related = (await _mediator.Send(new GetCountryByIdQuery(key))).Select(x => x.Commissions).SingleOrDefault();
        if (related is null)
        {
            return NotFound();
        }
        
        IList<System.Uri> references = new List<System.Uri>();
        foreach (var item in related)
        {
            references.Add(new System.Uri($"Commissions/{item.Id}", UriKind.Relative));
        }
        return Ok(references);
    }
    
    public async Task<ActionResult> DeleteRefToCommissions([FromRoute] System.String key, [FromRoute] System.Int64 relatedKey)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var deletedRef = await _mediator.Send(new DeleteRefCountryToCommissionsCommand(new CountryKeyDto(key), new CommissionKeyDto(relatedKey)));
        if (!deletedRef)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    public virtual async Task<ActionResult> PostToCommissions([FromRoute] System.String key, [FromBody] CommissionCreateDto commission)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        commission.CountryId = key;
        var createdKey = await _mediator.Send(new CreateCommissionCommand(commission, _cultureCode));
        
        var createdItem = (await _mediator.Send(new GetCommissionByIdQuery(createdKey.keyId))).SingleOrDefault();
        
        return Created(createdItem);
    }
    
    [EnableQuery]
    public virtual async Task<ActionResult<IQueryable<CommissionDto>>> GetCommissions(System.String key)
    {
        var entity = (await _mediator.Send(new GetCountryByIdQuery(key))).SelectMany(x => x.Commissions);
        if (!entity.Any())
        {
            return NotFound();
        }
        return Ok(entity);
    }
    
    [EnableQuery]
    [HttpGet("/api/Countries/{key}/Commissions/{relatedKey}")]
    public virtual async Task<SingleResult<CommissionDto>> GetCommissionsNonConventional(System.String key, System.Int64 relatedKey)
    {
        var related = (await _mediator.Send(new GetCountryByIdQuery(key))).SelectMany(x => x.Commissions).Where(x => x.Id == relatedKey);
        if (!related.Any())
        {
            return SingleResult.Create<CommissionDto>(Enumerable.Empty<CommissionDto>().AsQueryable());
        }
        return SingleResult.Create(related);
    }
    
    [HttpPut("/api/Countries/{key}/Commissions/{relatedKey}")]
    public virtual async Task<ActionResult<CommissionDto>> PutToCommissionsNonConventional(System.String key, System.Int64 relatedKey, [FromBody] CommissionUpdateDto commission)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var related = (await _mediator.Send(new GetCountryByIdQuery(key))).SelectMany(x => x.Commissions).Any(x => x.Id == relatedKey);
        if (!related)
        {
            return NotFound();
        }
        
        var etag = Request.GetDecodedEtagHeader();
        var updated = await _mediator.Send(new UpdateCommissionCommand(relatedKey, commission, _cultureCode, etag));
        if (updated == null)
        {
            return NotFound();
        }
        
        return Ok();
    }
    
    [HttpDelete("/api/Countries/{key}/Commissions/{relatedKey}")]
    public async Task<ActionResult> DeleteToCommissions([FromRoute] System.String key, [FromRoute] System.Int64 relatedKey)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var related = (await _mediator.Send(new GetCountryByIdQuery(key))).SelectMany(x => x.Commissions).Any(x => x.Id == relatedKey);
        if (!related)
        {
            return NotFound();
        }
        
        var etag = Request.GetDecodedEtagHeader();
        var deleted = await _mediator.Send(new DeleteCommissionByIdCommand(relatedKey, etag));
        if (!deleted)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    public async Task<ActionResult> CreateRefToVendingMachines([FromRoute] System.String key, [FromRoute] System.Guid relatedKey)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var createdRef = await _mediator.Send(new CreateRefCountryToVendingMachinesCommand(new CountryKeyDto(key), new VendingMachineKeyDto(relatedKey)));
        if (!createdRef)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    [HttpPut("/api/Countries/{key}/VendingMachines/$ref")]
    public async Task<ActionResult> UpdateRefToVendingMachinesNonConventional([FromRoute] System.String key, [FromBody] ReferencesDto<System.Guid> referencesDto)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var relatedKeysDto = referencesDto.References.Select(x => new VendingMachineKeyDto(x)).ToList();
        var updatedRef = await _mediator.Send(new UpdateRefCountryToVendingMachinesCommand(new CountryKeyDto(key), relatedKeysDto));
        if (!updatedRef)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    public async Task<ActionResult> GetRefToVendingMachines([FromRoute] System.String key)
    {
        var related = (await _mediator.Send(new GetCountryByIdQuery(key))).Select(x => x.VendingMachines).SingleOrDefault();
        if (related is null)
        {
            return NotFound();
        }
        
        IList<System.Uri> references = new List<System.Uri>();
        foreach (var item in related)
        {
            references.Add(new System.Uri($"VendingMachines/{item.Id}", UriKind.Relative));
        }
        return Ok(references);
    }
    
    public async Task<ActionResult> DeleteRefToVendingMachines([FromRoute] System.String key, [FromRoute] System.Guid relatedKey)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var deletedRef = await _mediator.Send(new DeleteRefCountryToVendingMachinesCommand(new CountryKeyDto(key), new VendingMachineKeyDto(relatedKey)));
        if (!deletedRef)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    public async Task<ActionResult> DeleteRefToVendingMachines([FromRoute] System.String key)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var deletedAllRef = await _mediator.Send(new DeleteAllRefCountryToVendingMachinesCommand(new CountryKeyDto(key)));
        if (!deletedAllRef)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    public virtual async Task<ActionResult> PostToVendingMachines([FromRoute] System.String key, [FromBody] VendingMachineCreateDto vendingMachine)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        vendingMachine.CountryId = key;
        var createdKey = await _mediator.Send(new CreateVendingMachineCommand(vendingMachine, _cultureCode));
        
        var createdItem = (await _mediator.Send(new GetVendingMachineByIdQuery(createdKey.keyId))).SingleOrDefault();
        
        return Created(createdItem);
    }
    
    [EnableQuery]
    public virtual async Task<ActionResult<IQueryable<VendingMachineDto>>> GetVendingMachines(System.String key)
    {
        var entity = (await _mediator.Send(new GetCountryByIdQuery(key))).SelectMany(x => x.VendingMachines);
        if (!entity.Any())
        {
            return NotFound();
        }
        return Ok(entity);
    }
    
    [EnableQuery]
    [HttpGet("/api/Countries/{key}/VendingMachines/{relatedKey}")]
    public virtual async Task<SingleResult<VendingMachineDto>> GetVendingMachinesNonConventional(System.String key, System.Guid relatedKey)
    {
        var related = (await _mediator.Send(new GetCountryByIdQuery(key))).SelectMany(x => x.VendingMachines).Where(x => x.Id == relatedKey);
        if (!related.Any())
        {
            return SingleResult.Create<VendingMachineDto>(Enumerable.Empty<VendingMachineDto>().AsQueryable());
        }
        return SingleResult.Create(related);
    }
    
    [HttpPut("/api/Countries/{key}/VendingMachines/{relatedKey}")]
    public virtual async Task<ActionResult<VendingMachineDto>> PutToVendingMachinesNonConventional(System.String key, System.Guid relatedKey, [FromBody] VendingMachineUpdateDto vendingMachine)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var related = (await _mediator.Send(new GetCountryByIdQuery(key))).SelectMany(x => x.VendingMachines).Any(x => x.Id == relatedKey);
        if (!related)
        {
            return NotFound();
        }
        
        var etag = Request.GetDecodedEtagHeader();
        var updated = await _mediator.Send(new UpdateVendingMachineCommand(relatedKey, vendingMachine, _cultureCode, etag));
        if (updated == null)
        {
            return NotFound();
        }
        
        return Ok();
    }
    
    [HttpDelete("/api/Countries/{key}/VendingMachines/{relatedKey}")]
    public async Task<ActionResult> DeleteToVendingMachines([FromRoute] System.String key, [FromRoute] System.Guid relatedKey)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var related = (await _mediator.Send(new GetCountryByIdQuery(key))).SelectMany(x => x.VendingMachines).Any(x => x.Id == relatedKey);
        if (!related)
        {
            return NotFound();
        }
        
        var etag = Request.GetDecodedEtagHeader();
        var deleted = await _mediator.Send(new DeleteVendingMachineByIdCommand(relatedKey, etag));
        if (!deleted)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    [HttpDelete("/api/Countries/{key}/VendingMachines")]
    public async Task<ActionResult> DeleteToVendingMachines([FromRoute] System.String key)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var related = (await _mediator.Send(new GetCountryByIdQuery(key))).Select(x => x.VendingMachines).SingleOrDefault();
        if (related == null)
        {
            return NotFound();
        }
        
        var etag = Request.GetDecodedEtagHeader();
        foreach(var item in related)
        {
            await _mediator.Send(new DeleteVendingMachineByIdCommand(item.Id, etag));
        }
        return NoContent();
    }
    
    public async Task<ActionResult> CreateRefToCustomers([FromRoute] System.String key, [FromRoute] System.Int64 relatedKey)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var createdRef = await _mediator.Send(new CreateRefCountryToCustomersCommand(new CountryKeyDto(key), new CustomerKeyDto(relatedKey)));
        if (!createdRef)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    [HttpPut("/api/Countries/{key}/Customers/$ref")]
    public async Task<ActionResult> UpdateRefToCustomersNonConventional([FromRoute] System.String key, [FromBody] ReferencesDto<System.Int64> referencesDto)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var relatedKeysDto = referencesDto.References.Select(x => new CustomerKeyDto(x)).ToList();
        var updatedRef = await _mediator.Send(new UpdateRefCountryToCustomersCommand(new CountryKeyDto(key), relatedKeysDto));
        if (!updatedRef)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    public async Task<ActionResult> GetRefToCustomers([FromRoute] System.String key)
    {
        var related = (await _mediator.Send(new GetCountryByIdQuery(key))).Select(x => x.Customers).SingleOrDefault();
        if (related is null)
        {
            return NotFound();
        }
        
        IList<System.Uri> references = new List<System.Uri>();
        foreach (var item in related)
        {
            references.Add(new System.Uri($"Customers/{item.Id}", UriKind.Relative));
        }
        return Ok(references);
    }
    
    public async Task<ActionResult> DeleteRefToCustomers([FromRoute] System.String key, [FromRoute] System.Int64 relatedKey)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var deletedRef = await _mediator.Send(new DeleteRefCountryToCustomersCommand(new CountryKeyDto(key), new CustomerKeyDto(relatedKey)));
        if (!deletedRef)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    public async Task<ActionResult> DeleteRefToCustomers([FromRoute] System.String key)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var deletedAllRef = await _mediator.Send(new DeleteAllRefCountryToCustomersCommand(new CountryKeyDto(key)));
        if (!deletedAllRef)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    public virtual async Task<ActionResult> PostToCustomers([FromRoute] System.String key, [FromBody] CustomerCreateDto customer)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        customer.CountryId = key;
        var createdKey = await _mediator.Send(new CreateCustomerCommand(customer, _cultureCode));
        
        var createdItem = (await _mediator.Send(new GetCustomerByIdQuery(createdKey.keyId))).SingleOrDefault();
        
        return Created(createdItem);
    }
    
    [EnableQuery]
    public virtual async Task<ActionResult<IQueryable<CustomerDto>>> GetCustomers(System.String key)
    {
        var entity = (await _mediator.Send(new GetCountryByIdQuery(key))).SelectMany(x => x.Customers);
        if (!entity.Any())
        {
            return NotFound();
        }
        return Ok(entity);
    }
    
    [EnableQuery]
    [HttpGet("/api/Countries/{key}/Customers/{relatedKey}")]
    public virtual async Task<SingleResult<CustomerDto>> GetCustomersNonConventional(System.String key, System.Int64 relatedKey)
    {
        var related = (await _mediator.Send(new GetCountryByIdQuery(key))).SelectMany(x => x.Customers).Where(x => x.Id == relatedKey);
        if (!related.Any())
        {
            return SingleResult.Create<CustomerDto>(Enumerable.Empty<CustomerDto>().AsQueryable());
        }
        return SingleResult.Create(related);
    }
    
    [HttpPut("/api/Countries/{key}/Customers/{relatedKey}")]
    public virtual async Task<ActionResult<CustomerDto>> PutToCustomersNonConventional(System.String key, System.Int64 relatedKey, [FromBody] CustomerUpdateDto customer)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var related = (await _mediator.Send(new GetCountryByIdQuery(key))).SelectMany(x => x.Customers).Any(x => x.Id == relatedKey);
        if (!related)
        {
            return NotFound();
        }
        
        var etag = Request.GetDecodedEtagHeader();
        var updated = await _mediator.Send(new UpdateCustomerCommand(relatedKey, customer, _cultureCode, etag));
        if (updated == null)
        {
            return NotFound();
        }
        
        return Ok();
    }
    
    [HttpDelete("/api/Countries/{key}/Customers/{relatedKey}")]
    public async Task<ActionResult> DeleteToCustomers([FromRoute] System.String key, [FromRoute] System.Int64 relatedKey)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var related = (await _mediator.Send(new GetCountryByIdQuery(key))).SelectMany(x => x.Customers).Any(x => x.Id == relatedKey);
        if (!related)
        {
            return NotFound();
        }
        
        var etag = Request.GetDecodedEtagHeader();
        var deleted = await _mediator.Send(new DeleteCustomerByIdCommand(relatedKey, etag));
        if (!deleted)
        {
            return NotFound();
        }
        
        return NoContent();
    }
    
    [HttpDelete("/api/Countries/{key}/Customers")]
    public async Task<ActionResult> DeleteToCustomers([FromRoute] System.String key)
    {
        if (!ModelState.IsValid)
        {
            throw new Nox.Exceptions.BadRequestException(ModelState);
        }
        
        var related = (await _mediator.Send(new GetCountryByIdQuery(key))).Select(x => x.Customers).SingleOrDefault();
        if (related == null)
        {
            return NotFound();
        }
        
        var etag = Request.GetDecodedEtagHeader();
        foreach(var item in related)
        {
            await _mediator.Send(new DeleteCustomerByIdCommand(item.Id, etag));
        }
        return NoContent();
    }
    
    #endregion
    
}
