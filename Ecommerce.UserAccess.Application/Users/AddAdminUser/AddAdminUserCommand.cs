using Ecommerce.BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;

namespace Ecommerce.UserAccess.Application.Users.AddAdminUser;

public sealed record AddAdminUserCommand(
    string Login,
    string Password,
    string FirstName,
    string LastName,
    string Email) : IQueryRequest<ErrorOr<Unit>>;
