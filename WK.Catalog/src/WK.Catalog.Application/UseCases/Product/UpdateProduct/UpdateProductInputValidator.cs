using FluentValidation;

namespace WK.Catalog.Application.UseCases.Product.UpdateProduct;
public class UpdateProductInputValidator
    : AbstractValidator<UpdateProductInput>
{
    public UpdateProductInputValidator()
        => RuleFor(x => x.Id).NotEmpty();
}
