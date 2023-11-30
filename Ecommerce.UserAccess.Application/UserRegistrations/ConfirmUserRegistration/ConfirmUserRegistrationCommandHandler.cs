using Ecommerce.BuildingBlocks.Application.Commands;
using Ecommerce.UserAccess.Domain.UserRegistrations;
using ErrorOr;
using MediatR;

namespace Ecommerce.UserAccess.Application.UserRegistrations.ConfirmUserRegistration;

internal sealed class ConfirmUserRegistrationCommandHandler : ICommandRequestHandler<ConfirmUserRegistrationCommand, ErrorOr<Unit>>
{
    private IUserRegistrationRepository _userRegistrationRepository;

    public ConfirmUserRegistrationCommandHandler
        (IUserRegistrationRepository userRegistrationRepository)
    {
        _userRegistrationRepository = userRegistrationRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(ConfirmUserRegistrationCommand command, CancellationToken cancellationToken)
    {
        UserRegistration? userRegistration =
            await _userRegistrationRepository.GetByIdAsync(
                UserRegistrationId.Create(command.UserRegistrationId));

        if (userRegistration is null)
        {
            return Error.NotFound("UserRegistration.NotFound", "The user registration with the id provided was not found");
        }

        userRegistration.Confirm();

        return Unit.Value;
    }
}
