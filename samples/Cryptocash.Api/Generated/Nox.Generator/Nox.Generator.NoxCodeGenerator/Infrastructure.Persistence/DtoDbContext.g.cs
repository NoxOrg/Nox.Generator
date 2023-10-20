﻿// Generated
#nullable enable

using Microsoft.EntityFrameworkCore;
using Nox;
using Nox.Solution;
using Nox.Extensions;
using Nox.Types.EntityFramework.Abstractions;
using Nox.Configuration;
using Nox.Infrastructure;

using Cryptocash.Application.Dto;

namespace Cryptocash.Infrastructure.Persistence;

internal class DtoDbContext : DbContext
{
    /// <summary>
    /// The Nox solution configuration.
    /// </summary>
    protected readonly NoxSolution _noxSolution;

    /// <summary>
    /// The database provider.
    /// </summary>
    protected readonly INoxDatabaseProvider _dbProvider;

    protected readonly INoxClientAssemblyProvider _clientAssemblyProvider;
    protected readonly INoxDtoDatabaseConfigurator _noxDtoDatabaseConfigurator;
    private readonly NoxCodeGenConventions _codeGeneratorState;

    public DtoDbContext(
        DbContextOptions<DtoDbContext> options,
        NoxSolution noxSolution,
        INoxDatabaseProvider databaseProvider,
        INoxClientAssemblyProvider clientAssemblyProvider,
        INoxDtoDatabaseConfigurator noxDtoDatabaseConfigurator,
        NoxCodeGenConventions codeGeneratorState
    ) : base(options)
    {
        _noxSolution = noxSolution;
        _dbProvider = databaseProvider;
        _clientAssemblyProvider = clientAssemblyProvider;
        _noxDtoDatabaseConfigurator = noxDtoDatabaseConfigurator;
        _codeGeneratorState = codeGeneratorState;
    }

    
        public DbSet<BookingDto> Bookings { get; set; } = null!;
        
        public DbSet<CommissionDto> Commissions { get; set; } = null!;
        
        public DbSet<CountryDto> Countries { get; set; } = null!;
        
        public DbSet<CurrencyDto> Currencies { get; set; } = null!;
        
        public DbSet<CustomerDto> Customers { get; set; } = null!;
        
        public DbSet<PaymentDetailDto> PaymentDetails { get; set; } = null!;
        
        public DbSet<TransactionDto> Transactions { get; set; } = null!;
        
        public DbSet<EmployeeDto> Employees { get; set; } = null!;
        
        public DbSet<LandLordDto> LandLords { get; set; } = null!;
        
        public DbSet<MinimumCashStockDto> MinimumCashStocks { get; set; } = null!;
        
        public DbSet<PaymentProviderDto> PaymentProviders { get; set; } = null!;
        
        public DbSet<VendingMachineDto> VendingMachines { get; set; } = null!;
        
        public DbSet<CashStockOrderDto> CashStockOrders { get; set; } = null!;
        

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if (_noxSolution.Infrastructure is { Persistence.DatabaseServer: not null })
        {
            _dbProvider.ConfigureDbContext(optionsBuilder, "Cryptocash", _noxSolution.Infrastructure!.Persistence.DatabaseServer);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureAuditable(modelBuilder);

        if (_noxSolution.Domain != null)
        {            
            foreach (var entity in _codeGeneratorState.Solution.Domain!.Entities)
            {
                // Ignore owned entities configuration as they are configured inside entity constructor
                if (entity.IsOwnedEntity)
                {
                    continue;
                }

                var type = _clientAssemblyProvider.GetType(_codeGeneratorState.GetEntityDtoTypeFullName(entity.Name + "Dto"));
                if (type != null)
                {
                    _noxDtoDatabaseConfigurator.ConfigureDto(
                        new Nox.Types.EntityFramework.EntityBuilderAdapter.EntityBuilderAdapter(modelBuilder.Entity(type)),
                        entity);
                }
                else
                {
                    throw new Exception($"Could not resolve type for {entity.Name}Dto");
                }
            }
        }
    }

    private void ConfigureAuditable(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookingDto>().HasQueryFilter(e => e.DeletedAtUtc == null);
        modelBuilder.Entity<CommissionDto>().HasQueryFilter(e => e.DeletedAtUtc == null);
        modelBuilder.Entity<CountryDto>().HasQueryFilter(e => e.DeletedAtUtc == null);
        modelBuilder.Entity<CurrencyDto>().HasQueryFilter(e => e.DeletedAtUtc == null);
        modelBuilder.Entity<CustomerDto>().HasQueryFilter(e => e.DeletedAtUtc == null);
        modelBuilder.Entity<PaymentDetailDto>().HasQueryFilter(e => e.DeletedAtUtc == null);
        modelBuilder.Entity<TransactionDto>().HasQueryFilter(e => e.DeletedAtUtc == null);
        modelBuilder.Entity<EmployeeDto>().HasQueryFilter(e => e.DeletedAtUtc == null);
        modelBuilder.Entity<LandLordDto>().HasQueryFilter(e => e.DeletedAtUtc == null);
        modelBuilder.Entity<MinimumCashStockDto>().HasQueryFilter(e => e.DeletedAtUtc == null);
        modelBuilder.Entity<PaymentProviderDto>().HasQueryFilter(e => e.DeletedAtUtc == null);
        modelBuilder.Entity<VendingMachineDto>().HasQueryFilter(e => e.DeletedAtUtc == null);
        modelBuilder.Entity<CashStockOrderDto>().HasQueryFilter(e => e.DeletedAtUtc == null);
    }
}