using FluentValidation;

namespace Ecommerce.Catalog.Application.Products.RemovePostProduct;

internal sealed class RemvePostProductCommandValidator 
    : AbstractValidator<RemovePostProductCommand>
{
    public RemvePostProductCommandValidator()
    {
        RuleFor(r => r.ProductId)
            .NotNull()
            .NotEmpty();
    }
}

