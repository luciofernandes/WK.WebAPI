using Refit;
using WK.Catalog.Web.Response;
using WK.Catalog.Web.Models;
using WK.Catalog.Web.Request;

namespace WK.Catalog.Web.Repositories.Interfaces
{
    public interface IProductRepository
    {
        [Get("/products")]
        Task<ProductsResponse> GetProducts();

        [Get("/products/{id}")]
        Task<ProductResponse> GetProduct(string id);

        [Delete("/products/{id}")]
        Task DeleteProduct(string id);

        [Put("/products/{id}")]
        Task<ProductResponse> UpdateCategory(string id, ProductRequest productRequest);

        [Post("/products")]
        Task<ProductResponse> InsertProduct(ProductRequest productRequest);

    }
}