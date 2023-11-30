using Ecommerce.BuildingBlocks.Application;
using Ecommerce.BuildingBlocks.Application.Commands;
using Ecommerce.Catalog.Domain.Comments;
using Ecommerce.Catalog.Domain.Products;
using ErrorOr;
using MediatR;

namespace Ecommerce.Catalog.Application.Comments.AddComment;

internal sealed class AddCommentCommandHandler : ICommandRequestHandler<AddCommentCommand, ErrorOr<Guid>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IProductRepository _productRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public AddCommentCommandHandler(ICommentRepository commentRepository,
        IProductRepository productRepository,
        IExecutionContextAccessor executionContextAccessor)
    {
        _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _executionContextAccessor = executionContextAccessor ?? throw new ArgumentNullException(nameof(executionContextAccessor)); ;
    }

    public async Task<ErrorOr<Guid>> Handle(AddCommentCommand command, CancellationToken cancellationToken)
    {
        var productId = ProductId.Create(command.ProductId);

        if (!await _productRepository.ExistsProductById(productId))
        {
            return Error.NotFound("Product.NotFound", "Product was not found");
        }

        var comment = Comment.Create(_executionContextAccessor.UserId, productId, command.Comment, DateTime.UtcNow);

        await _commentRepository.AddAsync(comment);

        return comment.CommentId.Value;
    }
}
