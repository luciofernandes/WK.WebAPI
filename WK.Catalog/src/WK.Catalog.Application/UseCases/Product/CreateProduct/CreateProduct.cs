using MediatR;
using WK.Catalog.Application.Interfaces;
using WK.Catalog.Application.UseCases.Product.Common;
using WK.Catalog.Domain.Repository;
using DomainEntity = WK.Catalog.Domain.Entity;


namespace WK.Catalog.Application.UseCases.Product.CreateProduct;
public class CreateProduct : ICreateProduct
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProduct(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork
    )
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<ProductModelOutput> Handle(
        CreateProductInput input, 
        CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.Get(input.CategoryId, cancellationToken);
 
        var product = new DomainEntity.Product(
            input.Name,
            input.Description,
            category
        );

        await _productRepository.Insert(product, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        
        return ProductModelOutput.FromProduct(product);
    }


}
