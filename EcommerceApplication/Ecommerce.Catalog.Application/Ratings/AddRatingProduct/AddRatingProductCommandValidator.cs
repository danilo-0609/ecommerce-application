using Ecommerce.Catalog.Domain.Products;
using FluentValidation;

namespace Ecommerce.Catalog.Application.Ratings.RatingProduct;

internal sealed class AddRatingProductCommandValidator
    : AbstractValidator<AddRatingProductCommand>
{
    private readonly IProductRepository _productRepository;

    public AddRatingProductCommandValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));

        RuleFor(r => r.RatingValue)
            .NotEmpty().WithMessage("Rating value is required")
            .NotNull().WithMessage("Rating value is required")
            .ExclusiveBetween(1, 5.1).WithMessage("Product rating must be a number between 1 and 5");

        RuleFor(r => r.ProductId)
            .NotNull().WithMessage("Product id is required")
            .NotEmpty().WithMessage("Product id is required")
            .MustAsync(async (productId, cancellationToken) =>
            {
                var id = ProductId.Create(productId);

                if (!await _productRepository.ExistsProductById(id))
                {
                    return false;
                }

                return true;

            }).WithErrorCode("Not found").WithMessage("The product with the id provided was not found");

        RuleFor(r => r.RatingComment)
            .MaximumLength(500).WithMessage("Rating comment cannot be greater than 500 letters");
    }
}
