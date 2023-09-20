﻿using Microsoft.EntityFrameworkCore;
using Nox.EntityFramework.SqlServer;
using Nox.Types.EntityFramework.Abstractions;
using Testcontainers.MsSql;
using TestWebApp.Infrastructure.Persistence;

namespace Nox.Integration.Tests.Fixtures;

public class NoxTestMsSqlContainerFixture : NoxTestContainerFixtureBase<MsSqlContainer>
{
    public NoxTestMsSqlContainerFixture()
    {
        _container = new MsSqlBuilder()
            .WithAutoRemove(true)
            .WithCleanUp(true)
            .Build();
    }

    public override INoxDatabaseProvider GetDatabaseProvider(IEnumerable<INoxTypeDatabaseConfigurator> configurators)
    {
        return new SqlServerDatabaseProvider(configurators);
    }

    protected override DbContextOptions<TestWebAppDbContext> CreateDbOptions(string connectionString)
    {
        return new DbContextOptionsBuilder<TestWebAppDbContext>()
                .UseSqlServer(connectionString)
                .Options;
    }

    protected override string GetConnectionString(MsSqlContainer container)
        => container.GetConnectionString();
}