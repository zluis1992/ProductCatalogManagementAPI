using System.Globalization;
using Application.Products;
using FluentValidation;

namespace Api.Validators;

public class ProductQueryFilterValidator : AbstractValidator<ProductQueryFilter>
{
    public ProductQueryFilterValidator()
    {
        RuleFor(x => x.Id)
            .Must(value => value == null || Guid.TryParse(value.ToString(), CultureInfo.InvariantCulture, out _))
            .WithMessage(string.Format(CultureInfo.InvariantCulture, Resources.validationRuleMessage, "Id", "Guid"));

        RuleFor(x => x.MinPrice)
            .Must(value => value is null or >= 0)
            .WithMessage("MinPrice must be a non-negative number.");

        RuleFor(x => x.MaxPrice)
            .Must(value => value is null or >= 0)
            .WithMessage("MaxPrice must be a non-negative number.");

        RuleFor(x => x)
            .Must(x => !x.MinPrice.HasValue || !x.MaxPrice.HasValue || x.MaxPrice >= x.MinPrice)
            .WithMessage("MaxPrice must be greater than or equal to MinPrice.");

        RuleFor(x => x)
            .Must(x => !x.MinPrice.HasValue || !x.MaxPrice.HasValue || x.MinPrice <= x.MaxPrice)
            .WithMessage("MinPrice must be less than or equal to MaxPrice.");
    }
}
