using FluentValidation;

namespace Ecommerce.Catalog.Application.Products.GetAllProductsBySellerPosted;

internal sealed class GetAllProductsPostedBySellerQueryValidator 
    : AbstractValidator<GetAllProductsPostedBySellerQuery>
{
    public GetAllProductsPostedBySellerQueryValidator()
    {
        RuleFor(r => r.SellerId)
            .NotEmpty().WithMessage("Seller Id is required")
            .NotNull().WithMessage("Seller Id is required");
    }
}
