using Ecommerce.BuildingBlocks.Domain;
using Ecommerce.UserAccess.Domain.UserRegistrations.DomainErrors;
using ErrorOr;

namespace Ecommerce.UserAccess.Domain.UserRegistrations.Rules;

internal sealed class UserLoginMustBeUniqueRule : IBusinessRule
{
    private readonly IUsersCounter _usersCounter;
    private readonly string _login;

    internal UserLoginMustBeUniqueRule(IUsersCounter usersCounter, string login)
    {
        _usersCounter = usersCounter;
        _login = login;
    }

    public Error Error => UserRegistrationErrors.LoginIsNotUnique;

    public bool IsBroken() => _usersCounter.CountUsersWithLogin(_login) > 1;

    public static string Message => "User login must be unique";
}
