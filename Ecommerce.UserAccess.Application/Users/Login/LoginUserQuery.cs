using Ecommerce.BuildingBlocks.Application.Queries;
using ErrorOr;

namespace Ecommerce.UserAccess.Application.Users.Login;

public sealed record LoginUserQuery(
    string Email,
    string Password) : IQueryRequest<ErrorOr<string>>;
