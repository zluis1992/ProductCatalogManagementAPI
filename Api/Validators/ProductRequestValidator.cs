using Application.Products;
using FluentValidation;

namespace Api.Validators;

public class ProductRequestValidator : AbstractValidator<ProductSaveCommand>
{
    private const int MIN_LENGTH = 8;

    public ProductRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(MIN_LENGTH);
        RuleFor(x => x.Price).NotEmpty().WithMessage("The price must not be empty.")
            .GreaterThanOrEqualTo(1).WithMessage("The price must be greater than or equal to 1.");
    }
}
