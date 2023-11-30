using Ecommerce.BuildingBlocks.Application.Queries;
using Ecommerce.Catalog.Domain.Products;
using ErrorOr;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Ecommerce.Catalog.Application.Products.GetProductsByProductType;

internal sealed class GetProductByProductTypeQueryHandler
    : IQueryRequestHandler<GetProductByProductTypeQuery, ErrorOr<IReadOnlyList<ProductDto>>>
{
    private readonly IProductRepository _productRepository;

    public GetProductByProductTypeQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<ProductDto>>> Handle(GetProductByProductTypeQuery query, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllProductsByProductType(query.ProductType, cancellationToken);

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
