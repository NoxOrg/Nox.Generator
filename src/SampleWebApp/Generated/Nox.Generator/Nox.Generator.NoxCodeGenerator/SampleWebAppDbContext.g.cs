﻿// Generated

#nullable enable

using Microsoft.EntityFrameworkCore;
using SampleWebApp.Domain;
using System.Reflection;

namespace SampleWebApp.Infrastructure.Persistence;

public partial class SampleWebAppDbContext : DbContext
{
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public SampleWebAppDbContext(
        DbContextOptions<SampleWebAppDbContext> options
        ) : base(options) { }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    
    public DbSet<Country> Countries;
    
    public DbSet<Currency> Currencies;
    
    public DbSet<Store> Stores;
    
    public DbSet<CountryLocalNames> CountryLocalNames;
    
    public static void RegisterDbContext(IServiceCollection services)
    {
        services.AddDbContext<SampleWebAppDbContext>();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var configurations = Assembly.GetExecutingAssembly();
        modelBuilder.ApplyConfigurationsFromAssembly(configurations);
        
        base.OnModelCreating(modelBuilder);
    }
}

