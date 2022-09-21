using WK.Catalog.Application.Interfaces;
using WK.Catalog.Application.UseCases.Category.Common;
using WK.Catalog.Domain.Repository;
using DomainEntity = WK.Catalog.Domain.Entity;


namespace WK.Catalog.Application.UseCases.Category.CreateCategory;
public class CreateCategory : ICreateCategory
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategory(
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork
    )
    {
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryModelOutput> Handle(
        CreateCategoryInput input, 
        CancellationToken cancellationToken)
    {
        var category = new DomainEntity.Category(
            input.Name
        );

        await _categoryRepository.Insert(category, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        
        return CategoryModelOutput.FromCategory(category);
    }
}
