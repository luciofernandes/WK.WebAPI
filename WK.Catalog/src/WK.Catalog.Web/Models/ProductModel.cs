using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace WK.Catalog.Web.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid? CategoryId { get; set; }
       // public List<SelectListItem>? Categories { get; set; }

    }
}
