using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.TestHost;
using Nox;
using Nox.Infrastructure;
using Nox.Solution;
using Nox.Types.EntityFramework.Abstractions;
using Testcontainers.MsSql;
using Testcontainers.PostgreSql;
using Xunit.Abstractions;

namespace ClientApi.Tests;

/// <summary>
/// Containerized Database instance for testing 
/// </summary>
public class TestDatabaseContainerService : IAsyncLifetime, ITestDatabaseService
{
#if DEBUG
    public static readonly DatabaseServerProvider DbProviderKind = DatabaseServerProvider.Postgres;
#else
    public static readonly DatabaseServerProvider DbProviderKind = DatabaseServerProvider.SqlServer;
#endif

    private DockerContainer? _dockerContainer;

    private NoxAppClient _noxAppClient = null!;

    public string ConnectionString { get; private set; } = string.Empty;

    public NoxAppClient GetNoxClient(ITestOutputHelper testOutput, bool enableMessagingTests, string? environment = null)
    {
        if (_noxAppClient == null)
        {
            var factory = new NoxWebApplicationFactory(testOutput, this, enableMessagingTests, environment)
                .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot("./tests/Nox.ClientApi.Tests")); 

            _noxAppClient = new NoxAppClient(factory!);                
        }
       
        return _noxAppClient;
    }

    public INoxDatabaseProvider GetDatabaseProvider(
        IEnumerable<INoxTypeDatabaseConfigurator> configurations,
        NoxCodeGenConventions noxSolutionCodeGeneratorState,
        INoxClientAssemblyProvider noxClientAssemblyProvider)
    {
        return DbProviderKind switch
        {
            DatabaseServerProvider.Postgres => new PostgreSqlTestProvider(ConnectionString, configurations, noxSolutionCodeGeneratorState),
            DatabaseServerProvider.SqlServer => new MsSqlTestProvider(ConnectionString, configurations, noxSolutionCodeGeneratorState),
            _ => throw new NotImplementedException($"{DbProviderKind} is not suported"),
        };
    }

    public async Task InitializeAsync()
    {
        Func<string> GetConnectionString;

        switch (DbProviderKind)
        {
            case DatabaseServerProvider.Postgres:
                var postgreContainer = new PostgreSqlBuilder()
                  .WithImage("postgres:14.7")
                  .WithDatabase("db")
                  .WithUsername("postgres")
                  .WithPassword("postgres")
                  .WithAutoRemove(true)
                  .WithCleanUp(true)
                  .Build();

                GetConnectionString = postgreContainer.GetConnectionString;
                _dockerContainer = postgreContainer;

                break;

            case DatabaseServerProvider.SqlServer:
                var sqlContainer = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2022-CU14-ubuntu-22.04")
                .WithAutoRemove(true)
                .WithCleanUp(true)
                .Build();

                GetConnectionString = ()=> sqlContainer.GetConnectionString().Replace("master", "clientapi"); ;
                _dockerContainer = sqlContainer;

                break;

            default:
                throw new NotImplementedException($"{DbProviderKind} is not supported");
        }

        await _dockerContainer.StartAsync();

        ConnectionString = GetConnectionString();
    }

    public Task DisposeAsync()
    {
        if (_dockerContainer is null)
        {
            throw new ApplicationException("Container has not been started yet. Invoke StartAsync method first.");
        }
        return _dockerContainer.DisposeAsync().AsTask();
    }

    public DatabaseServerProvider GetDatabaseServerProvider() => DbProviderKind;
}