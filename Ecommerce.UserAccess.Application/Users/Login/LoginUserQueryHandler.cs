using Ecommerce.BuildingBlocks.Application.Queries;
using Ecommerce.UserAccess.Application.Abstractions;
using Ecommerce.UserAccess.Domain.Users;
using ErrorOr;

namespace Ecommerce.UserAccess.Application.Users.Login;

internal sealed class LoginUserQueryHandler 
    : IQueryRequestHandler<LoginUserQuery, ErrorOr<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public LoginUserQueryHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<ErrorOr<string>> Handle(LoginUserQuery command, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByEmailAsync(command.Email);

        if (user is null)
        {
            return Error.NotFound("User.NotFound", "User was not found");
        }

        string token = _jwtProvider.Generate(user);

        return token;
    }
}
