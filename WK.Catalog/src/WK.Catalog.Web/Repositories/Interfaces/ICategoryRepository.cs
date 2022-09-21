using System.Threading.Tasks;
using Refit;
using WK.Catalog.Web.Response;
using WK.Catalog.Web.Models;
using WK.Catalog.Web.Results;
using WK.Catalog.Web.Request;
using Microsoft.AspNetCore.Mvc;

namespace WK.Catalog.Web.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        [Get("/categories")]
        Task<CategoriesResponse> GetCategories();

        [Get("/categories/{id}")]
        Task<CategoryResponse> GetCategory(string id);

        [Delete("/categories/{id}")]
        Task DeleteCategory(string id);

        [Put("/categories/{id}")]
        Task<CategoryResponse> UpdateCategory(string id, CategoryRequest categoryRequest);

        [Post("/categories")]
        Task<CategoryResponse> InserCategory(CategoryRequest categoryRequest);
    }
}