using Ecommerce.BuildingBlocks.Application.Queries;
using Ecommerce.Catalog.Domain.Comments;
using Ecommerce.Catalog.Domain.Products;
using ErrorOr;

namespace Ecommerce.Catalog.Application.Comments.GetAllComments;

internal sealed class GetAllCommentsQueryHandler 
    : IQueryRequestHandler<GetAllCommentsQuery, ErrorOr<List<CommentDto>>>
{
    private readonly ICommentRepository _commentRepository;

    public GetAllCommentsQueryHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
    }

    public async Task<ErrorOr<List<CommentDto>>> Handle(GetAllCommentsQuery query, CancellationToken cancellationToken)
    {
        var productId = ProductId.Create(query.ProductId);

        var comments = await _commentRepository.GetAllCommentsByProductId(productId);

        if (comments is null)
        {
            return Error.NotFound("Product.CommentsNotFound", "Comments in the product was not found");
        }

        List<CommentDto> commentDtos = new List<CommentDto>();

        foreach (var comment in comments)
        {
            var commentDto = new CommentDto(comment.UserId,
                comment.CommentText,
                comment.CreatedDateTime,
                comment.UpdatedDateTime);

            commentDtos.Add(commentDto);
        }

        return commentDtos;
    }
}
