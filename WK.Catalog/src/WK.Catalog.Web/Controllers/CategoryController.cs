using Microsoft.AspNetCore.Mvc;
using WK.Catalog.Web.Repositories.Interfaces;
using WK.Catalog.Web.Models;
using WK.Catalog.Web.Request;
using WK.Catalog.Web.Response;
using System.Text.Json.Nodes;

namespace WK.Catalog.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> ViewAsync()
        {
            var categories = await _categoryRepository.GetCategories();
            return View(categories.Data);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Details(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var category = await _categoryRepository.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category.Data);
        }
        public async Task<IActionResult> Edit(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var category = await _categoryRepository.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category.Data);
        }

        public async Task<ActionResult<CategoriesResponse>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetCategories();

            return categories;
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var category = await _categoryRepository.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category.Data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

             await _categoryRepository.DeleteCategory(id);
           

            return RedirectToAction("View","Category");
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> Create([Bind("Name")] CategoryRequest categoryRequest)
        {
            var category = await _categoryRepository.InserCategory(categoryRequest);
            return RedirectToAction("View", "Category");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("Name")] CategoryRequest categoryRequest)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {               
                var category = await _categoryRepository.UpdateCategory(id, categoryRequest);
                return RedirectToAction("View", "Category");
            }
            return View();
        }
      
    }

}
