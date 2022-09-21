using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WK.Catalog.Web.Results;
using WK.Catalog.Web.Response;
using WK.Catalog.Web.Repositories.Interfaces;
using WK.Catalog.Web.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using WK.Catalog.Web.Request;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace WK.Catalog.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        private ICategoryRepository _categoryRepository;

  
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository) {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> ViewAsync()
        {
            var products = await _productRepository.GetProducts();
            return View(products.Data);
        }

       public async Task<IActionResult> CreateAsync()
        {
            ViewBag.CategoryId = await PopulateCategoriesAsync(null);
            
            return View();
        }

        public async Task<IActionResult> Details(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var product = await _productRepository.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product.Data);
        }
        public async Task<IActionResult> Edit(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var product = await _productRepository.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            List<SelectListItem> categories = await PopulateCategoriesAsync(product.Data.Category.Id);

            ProductModel model = new ProductModel();
            model.Id = product.Data.Id;
            model.Name = product.Data.Name;
            model.Description = product.Data.Description;
            model.CategoryId = product.Data.Category.Id;
            ViewBag.CategoryId = categories;

            return View(model);
        }


        public async Task<List<SelectListItem>> PopulateCategoriesAsync(Guid? id)
        {
            
            List<SelectListItem> items = new List<SelectListItem>();
        
            var categories = await _categoryRepository.GetCategories();
            if (id == null)
            {
                items.Add(new SelectListItem
                {
                    Selected = true,
                    Text = "Selecione uma Categoria",
                    Value = "",
                });
            }
            foreach (var category in categories.Data)
            {
                items.Add(new SelectListItem
                {   
                    Selected = (category.Id == id),
                    Text = category.Name,
                    Value = category.Id.ToString(),
                });
            }
                   
            return items;
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var product = await _productRepository.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product.Data);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            await _productRepository.DeleteProduct(id);

            return RedirectToAction("View", "Product");
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> Create([Bind("Name,Description,CategoryId")] ProductRequest productRequest)
        {
            var category = await _productRepository.InsertProduct(productRequest);
            return RedirectToAction("View", "Product");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Description,CategoryId")] ProductRequest productRequest)
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var category = await _productRepository.UpdateCategory(id, productRequest);
                return RedirectToAction("View", "Product");
            }
            return View(productRequest);
        }
    }

}
