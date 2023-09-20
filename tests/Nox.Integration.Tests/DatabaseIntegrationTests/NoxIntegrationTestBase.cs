﻿using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.DependencyInjection;
using Nox.Application.Providers;
using Nox.EntityFramework.Postgres;
using Nox.Integration.Tests.Fixtures;
using Nox.Solution;
using Nox.Types.EntityFramework.Abstractions;
using System.Reflection;
using TestWebApp.Infrastructure.Persistence;

namespace Nox.Integration.Tests.DatabaseIntegrationTests;

public abstract class NoxIntegrationTestBase<TContainerFixture> : IClassFixture<TContainerFixture>
    where TContainerFixture : class, INoxTestContainer
{
    private const string _solutionSetupFileName = @"Nox.Integration.Tests.DatabaseIntegrationTests.Design.test.solution.nox.yaml";
    private readonly ServiceProvider _serviceProvider;
    private readonly INoxTestContainer _containerFixture;

    protected NoxIntegrationTestBase(INoxTestContainer containerFixture)
    {
        _containerFixture = containerFixture;

        var services = new ServiceCollection();
        services.AddNoxLib(Assembly.GetExecutingAssembly());

        _serviceProvider = services.BuildServiceProvider();

        RecreateDataContext();

        //DataContext.Database.EnsureDeleted();
        DataContext.Database.EnsureCreated();
    }

    protected TestWebAppDbContext DataContext { get; private set; } = default!;

    protected void RecreateDataContext()
    {
        var solutionFileSetup = GetSolutionSetup();
        var solution = new NoxSolutionBuilder()
                        .UseYamlFilesAndContent(solutionFileSetup)
                        .Build();

        var assemblyProvider = new NoxClientAssemblyProvider(Assembly.GetExecutingAssembly());
        var databaseProvider = new PostgresDatabaseProvider(_serviceProvider.GetServices<INoxTypeDatabaseConfigurator>());

        var options = _containerFixture.CreateDbOptions();

        DataContext = new TestWebAppDbContext(
                options,
                solution,
                databaseProvider,
                assemblyProvider,
                new DefaultUserProvider(),
                new DefaultSystemProvider());
    }

    private static Dictionary<string, Func<TextReader>> GetSolutionSetup()
    {
        return new Dictionary<string, Func<TextReader>>
        {
            [_solutionSetupFileName] = () =>
            {
                var assembly = Assembly.GetExecutingAssembly();
                using Stream stream = assembly.GetManifestResourceStream(_solutionSetupFileName)!;
                using StreamReader reader = new(stream!);
                string result = reader.ReadToEnd();
                return new StringReader(result);
            }
        };
    }
}