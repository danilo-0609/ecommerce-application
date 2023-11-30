using Ecommerce.UserAccess.Domain.UserRegistrations.Rules;
using ErrorOr;

namespace Ecommerce.UserAccess.Domain.UserRegistrations.DomainErrors;

internal static class UserRegistrationErrors
{
    internal static Error LoginIsNotUnique =>
        Error.Validation("UserRegistration.LoginIsNotUnique", UserLoginMustBeUniqueRule.Message);

    internal static Error RegistrationIsNotConfirmedYet =>
        Error.Validation("UserRegistration.NotConfirmedYet", UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule.Message);

    internal static Error AlreadyConfirmed =>
        Error.Validation("UserRegistration.ConfirmedAlready", UserRegistrationCannotBeConfirmedMoreThanOnceRule.Message);

    internal static Error ConfirmedAfterExpiration =>
        Error.Validation("UserRegistration.ConfirmedAfterExpiration", UserRegistrationCannotBeConfirmedAfterExpirationRule.Message);

    internal static Error AlreadyExpired =>
        Error.Validation("UserRegistration.ExpiredAlready", UserRegistrationCannotBeExpiredMoreThanOnceRule.Message);

}
