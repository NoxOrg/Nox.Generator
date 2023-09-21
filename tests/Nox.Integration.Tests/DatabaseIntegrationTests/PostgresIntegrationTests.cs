using Nox.Integration.Tests.Fixtures;

namespace Nox.Integration.Tests.DatabaseIntegrationTests;

[Collection("Sequential")]
public class PostgresIntegrationTests : NoxIntegrationTestBase<NoxTestPostgreContainerFixture>
{
    private readonly NoxCommonTestCaseFactory _noxCommonTestCases;

    public PostgresIntegrationTests(NoxTestPostgreContainerFixture containerFixture) : base(containerFixture)
    {
        _noxCommonTestCases = new NoxCommonTestCaseFactory(RecreateDataContext);
    }

    [Fact(Skip = "Fix issue with deploying db in container")]
    public void GeneratedEntity_Postgres_CanSaveAndReadFields_AllTypes()
    {
        _noxCommonTestCases.GenerateEntityCanSaveAndReadFieldsAllTypes();
    }

    [Fact(Skip = "Fix issue with deploying db in container")]
    public void UniqueConstraints_SameValue_ShouldThrowException()
    {
        _noxCommonTestCases.UniqueConstraintsSameValueShouldThrowException();
    }
}