namespace WK.Catalog.Api.ApiModels.Category;

public class UpdateCategoryApiInput
{
    public string Name { get; set; }

    public UpdateCategoryApiInput(
        string name
    ) {
        Name = name;
    }
}
