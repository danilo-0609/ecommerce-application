using Ecommerce.BuildingBlocks.Domain;
using Ecommerce.UserAccess.Domain.Common;
using Ecommerce.UserAccess.Domain.UserRegistrations.DomainErrors;
using Ecommerce.UserAccess.Domain.UserRegistrations.Events;
using Ecommerce.UserAccess.Domain.UserRegistrations.Rules;
using Ecommerce.UserAccess.Domain.Users;
using ErrorOr;
using MediatR;

namespace Ecommerce.UserAccess.Domain.UserRegistrations;

public sealed class UserRegistration : AggregateRoot<UserRegistrationId,  Guid>
{
    public UserRegistrationId UserRegistrationId { get; private set; }

    public string Login { get; private set; }

    public Password Password { get; private set; }

    public string Email { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string Name { get; private set; }

    public DateTime RegisteredDate { get; private set; }

    public UserRegistrationStatus Status { get; private set; }

    public DateTime? ConfirmedDate { get; private set; }

    public static ErrorOr<UserRegistration> RegisterNewUser(
        string login,
        string password,
        string email,
        string firstName,
        string lastName,
        IUsersCounter usersCounter,
        DateTime registerDate)
    {
        UserLoginMustBeUniqueRule loginMustBeUnique = new(usersCounter, login);

        if (loginMustBeUnique.IsBroken())
        {
            return UserRegistrationErrors.LoginIsNotUnique;
        }

        var userRegistrationId = UserRegistrationId.CreateUnique();

        var passwordHash = Password.Create(password);

        if (passwordHash is null)
        {
            return Error.Validation("Password.NotValid", "Password cannot be empty");
        }

        var userRegistration = new UserRegistration(
            userRegistrationId,
            login,
            passwordHash,
            email,
            firstName,
            lastName,
            $"{firstName} {lastName}",
            registerDate,
            UserRegistrationStatus.WaitingForConfirmation,
            null);

        userRegistration.AddDomainEvent(new NewUserRegisteredDomainEvent(
            Guid.NewGuid(),
            userRegistrationId,
            login,
            email,
            firstName,
            lastName,
            $"{firstName} {lastName}",
            registerDate));

        return userRegistration;
    }

    public ErrorOr<User> CreateUser()
    {
        UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule rule = new(Status);

        var check = CheckRule(rule);

        if (check.IsError)
        {
            return check.FirstError;
        }

        return User.CreateUserFromRegistration(
            UserRegistrationId,
            Login,
            Password,
            Email,
            FirstName,
            LastName,
            Name);
    }

    public ErrorOr<Unit> Confirm()
    {
        List<ErrorOr<Unit>> checkRules = new()
        {
            CheckRule(new UserRegistrationCannotBeConfirmedMoreThanOnceRule(Status)),
            CheckRule(new UserRegistrationCannotBeConfirmedAfterExpirationRule(Status))
        };

        if (checkRules.Any(k => k.IsError))
        {
            return checkRules.Select(ck => ck.FirstError).Single();
        }

        Status = UserRegistrationStatus.Confirmed;
        ConfirmedDate = DateTime.UtcNow;

        AddDomainEvent(new UserRegistrationConfirmedDomainEvent(Guid.NewGuid(), UserRegistrationId));

        return Unit.Value;
    }

    public ErrorOr<Unit> Expire()
    {
        var checkRule = CheckRule(new UserRegistrationCannotBeExpiredMoreThanOnceRule(Status));

        if (checkRule.IsError)
        {
            return checkRule.FirstError;
        }

        Status = UserRegistrationStatus.Expired;

        AddDomainEvent(new UserRegistrationExpiredDomainEvent(Guid.NewGuid(), UserRegistrationId));

        return Unit.Value;
    }

    private UserRegistration(UserRegistrationId id,
        string login,
        Password password,
        string email,
        string firstName,
        string lastName,
        string name,
        DateTime registeredDate,
        UserRegistrationStatus status,
        DateTime? confirmedDate)
    {
        UserRegistrationId = id;
        Login = login;
        Password = password;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Name = name;
        RegisteredDate = registeredDate;
        Status = status;
        ConfirmedDate = confirmedDate;
    }

    private UserRegistration() { }
}
