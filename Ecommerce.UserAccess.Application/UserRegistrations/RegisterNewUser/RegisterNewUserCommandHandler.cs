using Ecommerce.BuildingBlocks.Application.Commands;
using Ecommerce.UserAccess.Domain.UserRegistrations;
using ErrorOr;

namespace Ecommerce.UserAccess.Application.UserRegistrations.RegisterNewUser;

internal sealed class RegisterNewUserCommandHandler
    : ICommandRequestHandler<RegisterNewUserCommand, ErrorOr<Guid>>
{
    private readonly IUserRegistrationRepository _userRegistrationRepository;
    private readonly IUsersCounter _usersCounter;

    public RegisterNewUserCommandHandler(IUserRegistrationRepository userRegistrationRepository, 
        IUsersCounter usersCounter)
    {
        _userRegistrationRepository = userRegistrationRepository;
        _usersCounter = usersCounter;
    }

    public async Task<ErrorOr<Guid>> Handle(RegisterNewUserCommand command, CancellationToken cancellationToken)
    {
        ErrorOr<UserRegistration> userRegistration = 
            UserRegistration.RegisterNewUser(
            command.Login,
            command.Password,
            command.Email,
            command.FirstName,
            command.LastName,
            _usersCounter,
            DateTime.UtcNow);

        if (userRegistration.IsError)
        {
            return userRegistration.FirstError;
        }

        await _userRegistrationRepository.AddAsync(userRegistration.Value);

        return userRegistration.Value.UserRegistrationId.Value;
    }
}
