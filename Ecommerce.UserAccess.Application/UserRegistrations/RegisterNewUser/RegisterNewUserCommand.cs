using Ecommerce.BuildingBlocks.Application.Commands;
using ErrorOr;

namespace Ecommerce.UserAccess.Application.UserRegistrations.RegisterNewUser;

public sealed record RegisterNewUserCommand(string Login,
    string Password,
    string Email,
    string FirstName,
    string LastName) 
    : IQueryRequest<ErrorOr<Guid>>;
