using Ecommerce.Catalog.Domain.Products;
using FluentValidation;

namespace Ecommerce.Catalog.Application.Ratings.ChangeRating.ChangeCommentRating;

internal sealed class ChangeCommentRatingCommandValidator
    : AbstractValidator<ChangeCommentRatingCommand>
{
    private readonly IProductRepository _productRepository;

    public ChangeCommentRatingCommandValidator(IProductRepository productRepository)
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

        RuleFor(r => r.Comment)
            .NotEmpty().WithMessage("Comment is required")
            .NotNull().WithMessage("Comment is required")
            .MaximumLength(500).WithMessage("Rating comment cannot be greater than 500 letters");

    }
}
