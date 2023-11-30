using Ecommerce.BuildingBlocks.Application.Queries;
using ErrorOr;

namespace Ecommerce.Catalog.Application.Comments.GetAllComments;

public sealed record GetAllCommentsQuery(Guid ProductId) 
    : IQueryRequest<ErrorOr<List<CommentDto>>>;
