using System.Text.Json.Serialization;
using WK.Catalog.Application.UseCases.Category.UpdateCategory;

namespace WK.Catalog.Api.ApiModels.Product;

public class UpdateProductApiInput
{
    public string Name { get; set; }
    public string Description { get; set; }

    [JsonPropertyName("category_id")]
    public Guid CategoryId { get; set; }

    public UpdateProductApiInput(
        string name,
        string description,
        Guid categoryId
    ) {
        Name = name;
        Description = description;
        CategoryId = categoryId;
    }
}
