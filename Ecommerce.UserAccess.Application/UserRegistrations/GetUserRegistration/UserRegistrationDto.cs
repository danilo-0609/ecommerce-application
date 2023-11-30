namespace Ecommerce.UserAccess.Application.UserRegistrations.GetUserRegistration;

public sealed record UserRegistrationDto(
    Guid Id,
    string Login,
    string Email,
    string FirstName,
    string LastName,
    string Name,
    string StatusCode);
