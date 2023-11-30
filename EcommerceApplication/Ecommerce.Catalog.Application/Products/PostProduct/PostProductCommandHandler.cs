using Ecommerce.BuildingBlocks.Application;
using Ecommerce.BuildingBlocks.Application.Commands;
using Ecommerce.Catalog.Domain.Comments;
using Ecommerce.Catalog.Domain.Products;
using Ecommerce.Catalog.Domain.Products.ValueObjects;
using Ecommerce.Catalog.Domain.Ratings;
using ErrorOr;

namespace Ecommerce.Catalog.Application.Products.PostProduct;

internal sealed class PostProductCommandHandler
    : ICommandRequestHandler<PostProductCommand, ErrorOr<System.Guid>>
{
    private readonly IProductRepository _productRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public PostProductCommandHandler(IProductRepository productRepository, IExecutionContextAccessor executionContextAccessor)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(Product));
        _executionContextAccessor = executionContextAccessor ?? throw new ArgumentNullException(nameof(executionContextAccessor));
    }

    public async Task<ErrorOr<System.Guid>> Handle(
        PostProductCommand command,
        CancellationToken cancellationToken)
    {

        var product = Product.Publish(
            _executionContextAccessor.UserId,
            command.Name,
            command.Price,
            ProductType.Create(command.ProductType),
            command.inStock,
            DateTime.UtcNow,
            command.Description,
            command.Size,
            command.Color);

        if (product.IsError)
        {
            return product.FirstError;
        }

        await _productRepository
                .AddAsync(product.Value, cancellationToken);

        var productIdValue = product.Value.ProductId.Value;

        return productIdValue;
    }
}

