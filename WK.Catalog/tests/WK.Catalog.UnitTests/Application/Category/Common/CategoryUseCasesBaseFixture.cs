using WK.Catalog.Application.Interfaces;
using WK.Catalog.Domain.Repository;
using WK.Catalog.UnitTests.Common;
using Moq;
using DomainEntity = WK.Catalog.Domain.Entity;


namespace WK.Catalog.UnitTests.Application.Category.Common;
public abstract class CategoryUseCasesBaseFixture
    : BaseFixture
{

    public Mock<ICategoryRepository> GetRepositoryMock()
        => new();

    public Mock<IUnitOfWork> GetUnitOfWorkMock()
        => new();

    public string GetValidCategoryName()
    {
        var categoryName = "";
        while (categoryName.Length < 3)
            categoryName = Faker.Commerce.Categories(1)[0];
        if (categoryName.Length > 255)
            categoryName = categoryName[..255];
        return categoryName;
    }

    public DomainEntity.Category GetExampleCategory()
        => new(
            GetValidCategoryName()
        );
}
