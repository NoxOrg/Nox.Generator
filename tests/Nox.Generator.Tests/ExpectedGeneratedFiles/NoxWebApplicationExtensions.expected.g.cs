// Generated

#nullable enable

using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.OData.ModelBuilder;
using Nox;
using Nox.Solution;
using Nox.EntityFramework.SqlServer;
using Nox.Types.EntityFramework.Abstractions;
using TestWebApp.Infrastructure.Persistence;
using TestWebApp.Presentation.Api.OData;

public static class NoxWebApplicationBuilderExtension
{
    public static IServiceCollection AddNox(this IServiceCollection services)
                        {
                            return services.AddNox(null);
                        }
                        
    public static IServiceCollection AddNox(this IServiceCollection services, Action<ODataModelBuilder>? configureOData)
    {
        var noxSolution = services.AddNoxLib(Assembly.GetExecutingAssembly());
        services.TryAddNoxMessaging<TestWebAppDbContext>(noxSolution);
        services.AddNoxOdata(configureOData);
        services.AddSingleton(typeof(INoxClientAssemblyProvider), s => new NoxClientAssemblyProvider(Assembly.GetExecutingAssembly()));
        services.AddSingleton<DbContextOptions<TestWebAppDbContext>>();
        services.AddSingleton<INoxDatabaseConfigurator, SqlServerDatabaseProvider>();
        services.AddSingleton<INoxDatabaseProvider, SqlServerDatabaseProvider>();
        services.AddDbContext<TestWebAppDbContext>();
        services.AddDbContext<DtoDbContext>();
        return services;
    }
    
}
