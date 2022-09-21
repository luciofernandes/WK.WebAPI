using WK.Catalog.Application.Interfaces;
using WK.Catalog.Application.UseCases.Category.Common;
using WK.Catalog.Domain.Repository;

namespace WK.Catalog.Application.UseCases.Category.UpdateCategory;
public class UpdateCategory : IUpdateCategory
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _uinitOfWork;

    public UpdateCategory(
        ICategoryRepository categoryRepository, 
        IUnitOfWork uinitOfWork)
        => (_categoryRepository, _uinitOfWork) 
            = (categoryRepository, uinitOfWork);

    public async Task<CategoryModelOutput> Handle(UpdateCategoryInput request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.Get(request.Id, cancellationToken);
        category.Update(request.Name);
        await _categoryRepository.Update(category, cancellationToken);
        await _uinitOfWork.Commit(cancellationToken);
        return CategoryModelOutput.FromCategory(category);
    }
}
