using Ecommerce.BuildingBlocks.Domain;
using Ecommerce.UserAccess.Domain.UserRegistrations.DomainErrors;
using ErrorOr;

namespace Ecommerce.UserAccess.Domain.UserRegistrations.Rules;

internal sealed class UserRegistrationCannotBeConfirmedMoreThanOnceRule : IBusinessRule
{
    private readonly UserRegistrationStatus _actualRegistrationStatus;

    internal UserRegistrationCannotBeConfirmedMoreThanOnceRule(
        UserRegistrationStatus actualRegistrationStatus)
    {
        _actualRegistrationStatus = actualRegistrationStatus;
    }

    public Error Error => UserRegistrationErrors.AlreadyConfirmed;

    public bool IsBroken() => _actualRegistrationStatus == UserRegistrationStatus.Confirmed;

    public static string Message => "User registration cannot be confirmed more than once";
}
