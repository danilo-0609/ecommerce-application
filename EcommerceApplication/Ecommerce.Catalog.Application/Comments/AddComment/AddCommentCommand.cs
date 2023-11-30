using Ecommerce.BuildingBlocks.Application.Commands;
using ErrorOr;

namespace Ecommerce.Catalog.Application.Comments.AddComment;

public sealed record AddCommentCommand(Guid ProductId, 
    string Comment) : IQueryRequest<ErrorOr<Guid>>;
