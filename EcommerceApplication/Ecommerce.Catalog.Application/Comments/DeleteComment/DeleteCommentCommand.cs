using Ecommerce.BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;

namespace Ecommerce.Catalog.Application.Comments.DeleteComment;

public sealed record DeleteCommentCommand(Guid CommentId) : IQueryRequest<ErrorOr<Unit>>;
