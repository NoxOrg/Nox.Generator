﻿using MassTransit;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Neovolve.Logging.Xunit;
using Nox;
using Nox.Infrastructure;
using Nox.Solution;
using Nox.Types.EntityFramework.Abstractions;
using SqlKata.Compilers;
using Xunit.Abstractions;

namespace ClientApi.Tests;

public class NoxWebApplicationFactory : WebApplicationFactory<StartupFixture>
{
    private readonly ITestOutputHelper _testOutput;
    private readonly ITestDatabaseService _databaseService;
    private readonly bool _enableMessaging;
    private readonly string _environment = Environments.Production;

    public NoxWebApplicationFactory(
        ITestOutputHelper testOutput,
        ITestDatabaseService databaseService,
        bool enableMessaging,
        string? environment = null)
    {
        _testOutput = testOutput;
        _databaseService = databaseService;
        _enableMessaging = enableMessaging;

        if (environment != null)
        {
            _environment = environment;
        }
    }

    protected override IWebHostBuilder? CreateWebHostBuilder()
    {
        var host = WebHost.CreateDefaultBuilder(null!)
            .UseEnvironment(_environment)
            .UseStartup<StartupFixture>()
            // this extension makes it sure that our lambda will run after the Startup.ConfigureServices()
            // method has been executed.
            .ConfigureTestServices(services =>
            {
                //Override Db Provider and set container connection string
                RemoveIfExists(services, typeof(INoxDatabaseProvider));

                //For now we just support SQL Server
                //Our Strategy for creating the database in incompatable, because db is only created after services and app build
                //so hangfire is not able to create its schema
                //if (TestDatabaseContainerService.DbProviderKind == DatabaseServerProvider.SqlServer)
                //{
                //    services.AddNoxJobs(
                //        new Assembly[] { typeof(NoxWebApplicationFactory).Assembly },
                //        TestDatabaseContainerService.DbProviderKind,
                //        _databaseService.ConnectionString);
                //}

                services.AddSingleton(sp =>
                {
                    return _databaseService.GetDatabaseProvider(
                        sp.GetServices<INoxTypeDatabaseConfigurator>(),
                        sp.GetRequiredService<NoxCodeGenConventions>(),
                        sp.GetRequiredService<INoxClientAssemblyProvider>());
                });

                RemoveIfExists(services, typeof(Compiler));
                services.AddSingleton<Compiler>(sp =>
                {
                    return _databaseService.GetDatabaseServerProvider() switch
                    {
                        DatabaseServerProvider.SqlServer => new SqlServerCompiler(),
                        DatabaseServerProvider.SqLite => new SqliteCompiler(),
                        DatabaseServerProvider.Postgres => new PostgresCompiler(),
                        _ => throw new NotImplementedException()
                    };
                });

                if (_enableMessaging)
                    services.AddMassTransitTestHarness();

                //TODO Override NoxSolution with _dbProviderKind
            })
            .ConfigureLogging(opts => opts.AddXunit(_testOutput, new LoggingConfig
            {
                LogLevel = LogLevel.Error
            }));

        return host;
    }

    private void RemoveIfExists(IServiceCollection services, Type serviceType)
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == serviceType);
        if (descriptor != null)
        {
            services.Remove(descriptor);
        }
    }
}
