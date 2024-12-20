using Nox.Infrastructure;
using Nox.Integration.Tests.DataProviders;
using Nox.Solution;
using Nox.Types.EntityFramework.Abstractions;
using Testcontainers.MsSql;
using TestWebApp.Infrastructure.Persistence;

namespace Nox.Integration.Tests.Fixtures;

public class NoxTestMsSqlContainerFixture : NoxTestContainerFixtureBase<MsSqlContainer>
{
    private const string MasterDbName = "master";

    public NoxTestMsSqlContainerFixture()
    {
        _container = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-CU14-ubuntu-22.04")
            .WithAutoRemove(true)
            .WithCleanUp(true)
            .Build();
    }

    protected override INoxDatabaseProvider GetDatabaseProvider(
        IEnumerable<INoxTypeDatabaseConfigurator> configurators,
        NoxCodeGenConventions noxSolutionCodeGeneratorState)
    {
        return new MsSqlTestProvider(GetConnectionString(), configurators, noxSolutionCodeGeneratorState);
    }

    private string GetConnectionString()
    {
        //For development purposes

#pragma warning disable S125 // Sections of code should not be commented out
                            //return "Data Source=localhost;TrustServerCertificate=true;Initial Catalog=integrationtests;User ID=sa;password=Developer*123;";
        var connectionString = _container.GetConnectionString();
#pragma warning restore S125 // Sections of code should not be commented out
        if (connectionString.Contains(MasterDbName))
        {
            return connectionString.Replace(MasterDbName, nameof(AppDbContext));
        }

        return connectionString;
    }
}