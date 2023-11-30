using Ecommerce.BuildingBlocks.Application.Queries;
using ErrorOr;

namespace Ecommerce.UserAccess.Application.Users.GetUserById;

public sealed record GetUserByIdQuery(Guid UserId) : IQueryRequest<ErrorOr<UserDto>>;
