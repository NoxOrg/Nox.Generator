﻿using Microsoft.EntityFrameworkCore;
using Nox.Types.EntityFramework.Abstractions;
using TestWebApp.Infrastructure.Persistence;

namespace Nox.Integration.Tests.Fixtures;

public interface INoxTestContainer
{
    DbContextOptions<TestWebAppDbContext> CreateDbOptions();

    INoxDatabaseProvider GetDatabaseProvider(IEnumerable<INoxTypeDatabaseConfigurator> configurators);
}