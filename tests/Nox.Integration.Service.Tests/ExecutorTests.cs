using Microsoft.Extensions.DependencyInjection;
using Nox.Integration.Abstractions;

namespace Nox.Integration.Service.Tests;

public class ExecutorTests: IClassFixture<ServiceFixture>
{
    private readonly ServiceFixture _serviceFixture;

    public ExecutorTests(ServiceFixture serviceFixture)
    {
        _serviceFixture = serviceFixture;
    }
    
#if DEBUG
    [Fact]
#else
    [Fact (Skip = "Only available if sql server docker container is started")]
#endif    
    public async Task Can_Execute_a_simple_integration()
    {
        _serviceFixture.Configure("executor.solution.nox.yaml");
        var executor = _serviceFixture.ServiceProvider.GetRequiredService<IIntegrationExecutor>();
        var exception = await Record.ExceptionAsync(async () => await executor.ExecuteAsync("TestIntegration"));
        Assert.Null(exception);
    }
    
#if DEBUG
    [Fact]
#else
    [Fact (Skip = "Only available if sql server docker container is started")]
#endif    
    public async Task Can_Execute_an_integration_with_mapping()
    {
        _serviceFixture.Configure("mapping.solution.nox.yaml");
        var executor = _serviceFixture.ServiceProvider.GetRequiredService<IIntegrationExecutor>();
        var exception = await Record.ExceptionAsync(async () => await executor.ExecuteAsync("TestIntegration"));
        Assert.Null(exception);
    }
    
}