using Ecommerce.BuildingBlocks.Application.Events;
using Ecommerce.UserAccess.Domain.UserRegistrations;
using Ecommerce.UserAccess.Domain.UserRegistrations.Events;
using Ecommerce.UserAccess.Domain.Users;

namespace Ecommerce.UserAccess.Application.UserRegistrations.ConfirmUserRegistration;

internal sealed class UserRegistrationConfirmedHandler : IDomainEventHandler<UserRegistrationConfirmedDomainEvent>
{
    private readonly IUserRegistrationRepository _userRegistrationRepository;
    private readonly IUserRepository _userRepository;

    public UserRegistrationConfirmedHandler(IUserRegistrationRepository userRegistrationRepository, 
        IUserRepository userRepository)
    {
        _userRegistrationRepository = userRegistrationRepository;
        _userRepository = userRepository;
    }

    public async Task Handle(UserRegistrationConfirmedDomainEvent @event, CancellationToken cancellationToken)
    {
        UserRegistration? userRegistration = await _userRegistrationRepository
            .GetByIdAsync(@event.UserRegistrationId);

        ErrorOr.ErrorOr<User> user = userRegistration!.CreateUser();

        if (user.IsError)
        {
            return;
        }

        await _userRepository.AddAsync(user.Value);
    }
}
