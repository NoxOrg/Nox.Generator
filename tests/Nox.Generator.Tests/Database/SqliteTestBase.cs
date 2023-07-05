﻿using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Nox.Solution;
using Nox.Types.EntityFramework.Sqlite;
using SampleWebApp.Infrastructure.Persistence;
using System;

namespace NoxSourceGeneratorTests.DatabaseTests;

public abstract class SqliteTestBase : IDisposable
{
    private const string _inMemoryConnectionStringTemplate = "DataSource=:memory:";
    //private const string _inMemoryConnectionStringTemplate = @"DataSource=test_database_{0}.db";
    private static string _inMemoryConnectionString = string.Empty;
    private const string _testSolutionFile = @"../../../Database/Design/test.solution.nox.yaml";
    private readonly SqliteConnection _connection;

    protected SampleWebAppDbContext DbContext;

    protected SqliteTestBase()
    {
        _inMemoryConnectionString = string.Format(_inMemoryConnectionStringTemplate, DateTime.UtcNow.Ticks);
        _connection = new SqliteConnection(_inMemoryConnectionString);
        _connection.Open();
        DbContext = CreateDbContext(_connection);
    }

    private static SampleWebAppDbContext CreateDbContext(SqliteConnection connection)
    {
        var databaseConfigurator = new SqliteDatabaseConfigurator();
        var solution = new NoxSolutionBuilder()
            .UseYamlFile(_testSolutionFile)
            .Build();
        var options = new DbContextOptionsBuilder<SampleWebAppDbContext>()
            .UseSqlite(connection)
            .Options;
        var dbContext = new SampleWebAppDbContext(options, solution, databaseConfigurator);
        dbContext.Database.EnsureCreated();
        return dbContext;
    }

    internal void RecreateDbContext()
    {
        var previousDbContext = DbContext;
        DbContext = CreateDbContext(_connection);
        previousDbContext.Dispose();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        DbContext?.Dispose();
        _connection.Dispose();
    }
}