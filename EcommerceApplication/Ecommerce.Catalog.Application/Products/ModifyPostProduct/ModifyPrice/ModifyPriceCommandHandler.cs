﻿using Ecommerce.BuildingBlocks.Application.Commands;
using Ecommerce.Catalog.Domain.Products;
using ErrorOr;

namespace Ecommerce.Catalog.Application.Products.ModifyPostProduct.ModifyPrice;

internal sealed class ModifyPriceCommandHandler 
    : ICommandRequestHandler<ModifyPriceCommand, ErrorOr<Guid>>
{
    private readonly IProductRepository _productRepository;

    public ModifyPriceCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));

    }

    public async Task<ErrorOr<Guid>> Handle(ModifyPriceCommand command, CancellationToken cancellationToken)
    {
        var productId = ProductId.Create(command.ProductId);

        var product = await _productRepository.GetProductById(productId);

        if (product is null)
        {
            return Error.NotFound("Product.NotFound", "Product was not found");
        }

        var productUpdated = Product.Update(
            productId,
            product.SellerId,
            product.Name,
            command.Price,
            product.ProductType,
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