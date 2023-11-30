using Ecommerce.BuildingBlocks.Domain;
using Ecommerce.UserAccess.Domain.UserRegistrations.DomainErrors;
using ErrorOr;

namespace Ecommerce.UserAccess.Domain.UserRegistrations.Rules;

public sealed class UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule : IBusinessRule
{
    private readonly UserRegistrationStatus _actualRegistrationStatus;

    internal UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule(
        UserRegistrationStatus status)
    {
        _actualRegistrationStatus = status;
    }

    public bool IsBroken() => _actualRegistrationStatus != UserRegistrationStatus.Confirmed;

    public static string Message => "User cannot be created when registration is not confirmed";

    public Error Error => UserRegistrationErrors.RegistrationIsNotConfirmedYet;
}
