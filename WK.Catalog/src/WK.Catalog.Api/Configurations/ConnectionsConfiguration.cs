
using Microsoft.EntityFrameworkCore;
using WK.Catalog.Infra.Data.EF;

namespace WK.Catalog.Api.Configurations;

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

                ServerVersion.AutoDetect(connectionString),
                 b => b.MigrationsAssembly("WK.Catalog.Infra.Data.EF")
            )

            //options.UseSqlServer(connection, b => b.MigrationsAssembly("WK.Catalog.Api"))
        );
        return services;
    }


}
