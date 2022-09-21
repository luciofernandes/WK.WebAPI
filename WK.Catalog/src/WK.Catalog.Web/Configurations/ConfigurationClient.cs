
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using WK.Catalog.Web.Repositories;
using Refit;
using WK.Catalog.Web.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;

namespace WK.Catalog.Web.Configurations
{
    public static class ConfigurationClient
    {
  

        // This method gets called by the runtime. Use this method to add services to the container.
        public static IServiceCollection AddConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
     
            var uriAPI = configuration.GetValue<string>("ApiSettings:GatewayAddress");

            services.AddRefitClient<ICategoryRepository>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(uriAPI));

            services.AddRefitClient<IProductRepository>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(uriAPI));

            return services;
        }


     
    }
}
