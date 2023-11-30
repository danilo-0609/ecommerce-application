using Ecommerce.BuildingBlocks.Application.Commands;
using Ecommerce.Catalog.Domain.Products;
using Ecommerce.Catalog.Domain.Products.ValueObjects;
using ErrorOr;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Ecommerce.Catalog.Application.Products.ModifyPostProduct.ModifyProductType;

internal sealed class ModifyProductTypeCommandHandler 
    : ICommandRequestHandler<ModifyProductTypeCommand, ErrorOr<Guid>>
{
    private readonly IProductRepository _productRepository;

    public ModifyProductTypeCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<ErrorOr<Guid>> Handle(ModifyProductTypeCommand command, CancellationToken cancellationToken)
    {
        var productId = ProductId.Create(command.ProductId);

        var product = await _productRepository.GetProductById(productId);

        if (product is null)
        {
            return Error.NotFound("Product.NotFound", "Product was not found");
        }

        ProductType productType = ProductType.Create(command.ProductType);

        var productUpdated = Product.Update(
            productId,
            product.SellerId,
            product.Name,
            product.Price.Value,
            productType,
            product.InStock,
            product.CreatedDateTime,
            DateTime.UtcNow,
            product.Description,
            product.Size,
            product.Color);

        await _productRepository.UpdateAsync(productUpdated.Value);

        return productId.Value;
    }
}
