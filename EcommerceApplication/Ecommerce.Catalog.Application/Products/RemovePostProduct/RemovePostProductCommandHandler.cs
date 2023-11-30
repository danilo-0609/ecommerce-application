using Ecommerce.BuildingBlocks.Application.Commands;
using Ecommerce.Catalog.Domain.Products;
using ErrorOr;
using MediatR;

namespace Ecommerce.Catalog.Application.Products.RemovePostProduct;

internal sealed class RemovePostProductCommandHandler
    : ICommandRequestHandler<RemovePostProductCommand, ErrorOr<Unit>>
{
    private readonly IProductRepository _productRepository;

    public RemovePostProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<ErrorOr<Unit>> Handle(RemovePostProductCommand command, CancellationToken cancellationToken)
    {
        var productId = ProductId.Create(command.ProductId);

        if (await _productRepository.GetProductById(productId) is not Product product)
        {
            return Error.NotFound("Product.NotFound", "Product was not found");
        }

        product.Remove();
        
        await _productRepository.DeleteAsync(productId, cancellationToken);


        return Unit.Value;
    }
}
