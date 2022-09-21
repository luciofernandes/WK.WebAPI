using WK.Catalog.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace WK.Catalog.Infra.Data.EF.Configurations;

public static class ConnectionsConfiguration
{
    public static IServiceCollection AddAppConections(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbConnection(configuration);
        return services;
    }

    private static IServiceCollection AddDbConnection(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration
            .GetConnectionString("CatalogDb");
        services.AddDbContext<CatalogDbContext>(
            options => options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            ),
            ServiceLifetime.Transient
        );
       
        return services;
    }
}
