using Ecommerce.BuildingBlocks.Domain;
using Ecommerce.UserAccess.Domain.Common;
using Ecommerce.UserAccess.Domain.UserRegistrations;
using Ecommerce.UserAccess.Domain.Users.Events;
using ErrorOr;

namespace Ecommerce.UserAccess.Domain.Users;

public sealed class User : AggregateRoot<UserId, Guid>
{
    public UserId UserId { get; private set; }

    public string Login { get; private set; }

    public Password Password { get; private set; }

    public string Email { get; private set; }

    public bool IsActive { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string Name { get; private set; }

    public List<UserRole> Roles { get; private set; }

    public static ErrorOr<User> CreateAdmin(
        string login,
        string password,
        string email,
        string firstName,
        string lastName,
        string name)
    {

        Password? passwordHash = Password.Create(password);

        if (passwordHash is null)
        {
            return Error.Validation("Password.NotValid", "Password model not valid");
        }

        var user = new User(
            UserId.CreateUnique(),
            login,
            passwordHash,
            email,
            true,
            firstName,
            lastName,
            name,
            UserRole.Administrador);

        user.AddDomainEvent(new UserCreatedEvent(Guid.NewGuid(), user.UserId));

        return user;
    }

    internal static User CreateUserFromRegistration(
        UserRegistrationId userRegistrationId,
        string login,
        Password password,
        string email,
        string firstName,
        string lastName,
        string name)
    {
        var user = new User(
            UserId.Create(userRegistrationId.Value),
            login,
            password,
            email,
            true,
            firstName,
            lastName,
            name,
            UserRole.Customer);

        user.AddDomainEvent(new UserCreatedEvent(Guid.NewGuid(), user.UserId));

        return user;
    }

    private User(UserId userId, 
        string login, 
        Password password, 
        string email, 
        bool isActive, 
        string firstName, 
        string lastName, 
        string name, 
        UserRole role)
    {
        UserId = userId;
        Login = login;
        Password = password;
        Email = email;
        IsActive = isActive;
        FirstName = firstName;
        LastName = lastName;
        Name = name;

        Roles = new List<UserRole>();
        Roles.Add(role);
    }

    private User() { }
}
