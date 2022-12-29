using BuildingBlocks.Validation;
using FluentValidation;

namespace FluentPOS.Catalog.Application.Products.Features.Validators;

public class CreateProductCommandValidator : CustomValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Name)
           .NotEmpty()
           .MaximumLength(75);

        RuleFor(p => p.Price)
            .GreaterThanOrEqualTo(1);
    }
}
