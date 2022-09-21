
using MediatR;
using Newtonsoft.Json;
using WK.Catalog.Application.UseCases.Product.Common;
using WK.Catalog.Domain.Entity;

namespace WK.Catalog.Application.UseCases.Product.CreateProduct;
public class CreateProductInput : IRequest<ProductModelOutput>
{
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("description")]
    public string Description { get; set; }
    [JsonProperty("category_id")]
    public Guid CategoryId { get; set; }

    public CreateProductInput(
         string name,
         string description,
         Guid categoryId
       )
    {
        Name = name;
        Description = description;
        CategoryId = categoryId;
    }
}
