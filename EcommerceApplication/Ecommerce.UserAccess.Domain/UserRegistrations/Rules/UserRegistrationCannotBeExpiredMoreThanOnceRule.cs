using Ecommerce.BuildingBlocks.Domain;
using Ecommerce.UserAccess.Domain.UserRegistrations.DomainErrors;
using ErrorOr;

namespace Ecommerce.UserAccess.Domain.UserRegistrations.Rules;

internal sealed class UserRegistrationCannotBeExpiredMoreThanOnceRule : IBusinessRule
{
    private readonly UserRegistrationStatus _actualRegistrationStatus;

    internal UserRegistrationCannotBeExpiredMoreThanOnceRule(
        UserRegistrationStatus actualRegistrationStatus)
    {
        _actualRegistrationStatus = actualRegistrationStatus;
    }

    public Error Error => UserRegistrationErrors.AlreadyExpired;

    public bool IsBroken() => _actualRegistrationStatus == UserRegistrationStatus.Expired;

    public static string Message => "User registration cannot be expired more than once";
}
