using Ecommerce.Catalog.Domain.Products;
using FluentValidation;

namespace Ecommerce.Catalog.Application.Ratings.GetAllRatingProductsByProductId;

internal sealed class GetAllRatingProductsByProductIdQueryValidator 
    : AbstractValidator<GetAllRatingProductsByProductIdQuery>
{
    private readonly IProductRepository _productRepository;

    public GetAllRatingProductsByProductIdQueryValidator(IProductRepository productRepository)
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
        _productRepository = productRepository;

    }
}
