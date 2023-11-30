using FluentValidation;

namespace Ecommerce.Catalog.Application.Products.PostProduct;

internal sealed class PostProductCommandValidator 
    : AbstractValidator<PostProductCommand>
{
    public PostProductCommandValidator()
    {
        RuleFor(r => r.Name)
            .NotNull().WithMessage("Name is required")
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(3)
            .MaximumLength(300);

        RuleFor(r => r.Price)
            .NotEmpty().WithMessage("Price is required")
            .NotNull().WithMessage("Price is required")
            .ExclusiveBetween(0M, 100000000M)
            .WithMessage("Price cannot be 0 or 100000000");

        RuleFor(r => r.Description)
            .NotEmpty().WithMessage("Description is required")
            .NotNull().WithMessage("Description is required")
            .MaximumLength(2500)
            .MinimumLength(5);

        RuleFor(r => r.inStock)
            .NotNull().WithMessage("Amount of products in stock is required")
            .NotEmpty().WithMessage("Amount of products in stock is required");

        RuleFor(r => r.Size)
            .NotEmpty().WithMessage("Size is required")
            .NotNull().WithMessage("Size is required")
            .MinimumLength(1)
            .MaximumLength(10);

        RuleFor(r => r.Color)
            .NotNull().WithMessage("Color is required")
            .NotEmpty().WithMessage("Color is required")
            .MinimumLength(2)
            .MaximumLength(50);

        RuleFor(r => r.ProductType)
            .NotEmpty().WithMessage("Product type is required")
            .NotNull().WithMessage("Product type is required")
            .MinimumLength(2)
            .MaximumLength(50);

    }
}
