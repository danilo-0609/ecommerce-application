using Ecommerce.BuildingBlocks.Application.Commands;
using Ecommerce.Catalog.Domain.Products;
using ErrorOr;
using MediatR;

namespace Ecommerce.Catalog.Application.Products.SellProducts;

internal sealed class SellProductsCommandHandler : ICommandRequestHandler<SellProductsCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;

    public SellProductsCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(SellProductsCommand command, CancellationToken cancellationToken)
    {
        Product? product = await  _productRepository.GetProductById(ProductId.Create(command.ProductId));

        if (product is null)
        {
            return Error.Validation("Product.NotFound", "Product was not found");
        }

        product.Sell(command.AmountOfProducts);

        var update = Product.Update(
            product.ProductId,
            product.SellerId,
            product.Name,
            product.Price.Value,
            product.ProductType,
            product.InStock,
            product.CreatedDateTime,
            DateTime.UtcNow,
            product.Description,
            product.Size,
            product.Color);

        await _productRepository.UpdateAsync(update.Value);

        return Unit.Value;
    }
}
