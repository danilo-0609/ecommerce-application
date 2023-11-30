using Ecommerce.BuildingBlocks.Application.Queries;
using Ecommerce.Catalog.Domain.Products;
using ErrorOr;

namespace Ecommerce.Catalog.Application.Products.GetAllProductsBySellerPosted;

internal sealed class GetAllProductsPostedBySellerQueryHandler 
    : IQueryRequestHandler<GetAllProductsPostedBySellerQuery, ErrorOr<IReadOnlyList<ProductDto>>>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsPostedBySellerQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<ProductDto>>> Handle(GetAllProductsPostedBySellerQuery query, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllProductsBySellerAsync(query.SellerId, cancellationToken);

        if (products is null) 
        {
            return Error.NotFound("Seller.NotFound", "Seller was not found");
        }

        var productsList = new List<ProductDto>();
             
        products.ForEach(product =>
        {
            var productDto = new ProductDto(
                product.ProductId.Value,
                product.SellerId,
                product.Name,
                product.Price.Value,
                product.Description,
                product.Size,
                product.Color,
                product.ProductType.Value,
                product.InStock);

                productsList.Add(productDto);
        });

        return productsList.AsReadOnly();
    }
}