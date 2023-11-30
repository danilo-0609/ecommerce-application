using Ecommerce.Catalog.Domain.Products;
using FluentValidation;

namespace Ecommerce.Catalog.Application.Ratings.ChangeRating.UpdateRatingValue;

internal sealed class UpdateRatingValueCommandValidator 
    : AbstractValidator<UpdateRatingValueCommand>
{
    private readonly IProductRepository _productRepository;

    public UpdateRatingValueCommandValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(r => r.ProductId)
            .NotNull()
            .NotEmpty()
            .MustAsync(async (productId, cancellationToken) =>
            {
                var id = ProductId.Create(productId);

                if (!await _productRepository.ExistsProductById(id))
                {
                    return false;
                }

                return true;

            }).WithErrorCode("Not found").WithMessage("The product with the id provided was not found");

        RuleFor(r => r.RatingId)
            .NotNull()
            .NotEmpty();

        RuleFor(r => r.RatingValue)
            .NotEmpty().WithMessage("Rating value is required")
            .NotNull().WithMessage("Rating value is required")
            .ExclusiveBetween(1, 5.1).WithMessage("Product rating must be a number between 1 and 5");
    }
}
