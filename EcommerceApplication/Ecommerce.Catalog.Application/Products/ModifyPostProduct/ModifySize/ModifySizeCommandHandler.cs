using Ecommerce.BuildingBlocks.Application.Commands;
using Ecommerce.Catalog.Domain.Products;
using ErrorOr;

namespace Ecommerce.Catalog.Application.Products.ModifyPostProduct.ModifySize;

internal sealed class ModifySizeCommandHandler 
    : ICommandRequestHandler<ModifySizeCommand, ErrorOr<Guid>>
{
    private readonly IProductRepository _productRepository;

    public ModifySizeCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<ErrorOr<Guid>> Handle(ModifySizeCommand command, CancellationToken cancellationToken)
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
            product.Price.Value,
            product.ProductType,
            product.InStock,
            product.CreatedDateTime,
            DateTime.UtcNow,
            product.Description,
            command.Size,
            product.Color);

        await _productRepository.UpdateAsync(productUpdated.Value);

        return productId.Value;

    }
}
