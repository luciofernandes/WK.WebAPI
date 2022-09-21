using DomainEntity = WK.Catalog.Domain.Entity;

namespace WK.Catalog.Application.UseCases.Category.Common;
public class CategoryModelOutput
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public CategoryModelOutput(
        Guid id,
        string name
    )
    {
        Id = id;
        Name = name;

    }

    public static CategoryModelOutput FromCategory(DomainEntity.Category category)
        => new(
            category.Id,
            category.Name
        );
}
