namespace Ecommerce.UserAccess.Application.Users;

public sealed record UserDto(Guid UserId,
    string Login,
    string Name);
