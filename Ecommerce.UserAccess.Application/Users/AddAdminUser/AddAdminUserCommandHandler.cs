using Ecommerce.BuildingBlocks.Application.Commands;
using Ecommerce.UserAccess.Domain.Users;
using ErrorOr;
using MediatR;

namespace Ecommerce.UserAccess.Application.Users.AddAdminUser;

internal sealed class AddAdminUserCommandHandler : ICommandRequestHandler<AddAdminUserCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository _userRepository;

    public AddAdminUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(AddAdminUserCommand command, CancellationToken cancellationToken)
    {
        ErrorOr<User> user = User.CreateAdmin(command.Login,
            command.Password,
            command.Email,
            command.FirstName,
            command.LastName,
            $"{command.FirstName} {command.LastName}");
    
        if (user.IsError)
        {
            return user.FirstError;
        }

        await _userRepository.AddAsync(user.Value);

        return Unit.Value;
    }

}
