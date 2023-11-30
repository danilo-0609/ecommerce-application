using FluentValidation;

namespace Ecommerce.UserAccess.Application.UserRegistrations.ConfirmUserRegistration;

internal sealed class ConfirmUserRegistrationCommandValidator 
    : AbstractValidator<ConfirmUserRegistrationCommand>
{
    public ConfirmUserRegistrationCommandValidator()
    {
        RuleFor(r => r.UserRegistrationId)
            .NotEmpty().WithMessage("User registration id is required")
            .NotNull().WithMessage("User registration id is required");
    }
}
