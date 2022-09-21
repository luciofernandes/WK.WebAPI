using FluentAssertions;
using WK.Catalog.Domain.Exceptions;
using Xunit;
using DomainEntity = WK.Catalog.Domain.Entity;

namespace WK.Catalog.UnitTests.Domain.Entity.Category
{
    [Collection(nameof(CategoryTestFixture))]
    public class CategoryTest
    {
        private readonly CategoryTestFixture _categoryTestFixture;

        public CategoryTest(CategoryTestFixture categoryTestFixture)
            => _categoryTestFixture = categoryTestFixture;

        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "Category - Aggregates")]
        public void Instantiate()
        {    
            var valideCateory = _categoryTestFixture.GetValidCategory();

            var dateTimeBefore = DateTime.Now;
            
            var category = new DomainEntity.Category(valideCateory.Name);
            var dateTimeAfter = DateTime.Now.AddSeconds(1);

            category.Should().NotBeNull();
            category.Name.Should().Be(valideCateory.Name);
            category.Id.Should().NotBeEmpty();

        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        public void InstantiateErrorWhenNameIsEmpty(string? name)
        {
           Action action = () => new DomainEntity.Category(name!);
           action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name não pode ser vazio ou nulo");         
        }

        [Fact(DisplayName = nameof(Update))]
        [Trait("Domain", "Category - Aggregates")]
        public void Update()
        {
            var valideCateory = _categoryTestFixture.GetValidCategory();
            var newValues = _categoryTestFixture.GetValidCategory(); 
           

            valideCateory.Name.Should().Be(newValues.Name);
          
        }
    }
}
