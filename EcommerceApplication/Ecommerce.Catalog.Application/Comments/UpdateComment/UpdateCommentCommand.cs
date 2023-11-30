using Ecommerce.BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;

namespace Ecommerce.Catalog.Application.Comments.UpdateComment;

public sealed record UpdateCommentCommand(Guid ProductId,
    string Comment,
    Guid CommentId) : IQueryRequest<ErrorOr<Guid>>;
