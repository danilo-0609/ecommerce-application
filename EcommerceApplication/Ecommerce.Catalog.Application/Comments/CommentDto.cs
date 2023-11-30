using Ecommerce.Catalog.Domain.Common;

namespace Ecommerce.Catalog.Application.Comments;

public sealed record CommentDto(UserId UserId, 
    string comment, 
    DateTime CreatedDateTime, 
    DateTime? UpdatedDateTime);
