using WK.Catalog.Application.Interfaces;
using WK.Catalog.Application.UseCases.Category.CreateCategory;
using WK.Catalog.Application.UseCases.Product.CreateProduct;
using WK.Catalog.Domain.Repository;
using WK.Catalog.Infra.Data.EF;
using WK.Catalog.Infra.Data.EF.Repositories;
using MediatR;

namespace WK.Catalog.Api.Configurations;

public static class UseCasesConfiguration
{
    public static IServiceCollection AddUseCases(
        this IServiceCollection services
    )
    {
        services.AddMediatR(typeof(CreateCategory));
        services.AddMediatR(typeof(CreateProduct));
        services.AddRepositories();
        return services;
    }

    private static IServiceCollection AddRepositories(
            this IServiceCollection services
        )
    {
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }


}
