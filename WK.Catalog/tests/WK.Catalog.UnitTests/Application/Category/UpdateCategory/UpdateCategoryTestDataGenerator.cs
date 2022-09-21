using System.Collections.Generic;

namespace WK.Catalog.UnitTests.Application.Category.UpdateCategory;
public class UpdateCategoryTestDataGenerator
{
    public static IEnumerable<object[]> GetCategoriesToUpdate(int times = 10)
    {
        var fixture = new UpdateCategoryTestFixture();
        for (int indice = 0; indice < times; indice++)
        {
            var exampleCategory = fixture.GetExampleCategory();
            var exampleInput = fixture.GetValidInput(exampleCategory.Id);
            yield return new object[] {
                exampleCategory, exampleInput
            };
        }
    }

}
