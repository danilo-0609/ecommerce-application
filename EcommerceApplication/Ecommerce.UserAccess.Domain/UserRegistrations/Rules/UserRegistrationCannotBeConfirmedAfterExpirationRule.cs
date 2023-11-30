using Ecommerce.BuildingBlocks.Domain;
using Ecommerce.UserAccess.Domain.UserRegistrations.DomainErrors;
using ErrorOr;

namespace Ecommerce.UserAccess.Domain.UserRegistrations.Rules;

internal sealed class UserRegistrationCannotBeConfirmedAfterExpirationRule : IBusinessRule
{
    private readonly UserRegistrationStatus _actualRegistrationStatus;

    internal UserRegistrationCannotBeConfirmedAfterExpirationRule(
        UserRegistrationStatus userRegistrationStatus)
    {
        _actualRegistrationStatus = userRegistrationStatus;
    }

    public Error Error => UserRegistrationErrors.ConfirmedAfterExpiration;

    public bool IsBroken() => _actualRegistrationStatus == UserRegistrationStatus.Expired;

    public static string Message => "User registration cannot be confirmed after expiration";
}
