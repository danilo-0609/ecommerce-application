using Ecommerce.BuildingBlocks.Application.Queries;
using Ecommerce.UserAccess.Domain.Users;
using ErrorOr;

namespace Ecommerce.UserAccess.Application.Users.GetUserById;

internal sealed class GetUserByIdQueryHandler : IQueryRequestHandler<GetUserByIdQuery, ErrorOr<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<UserDto>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        UserId userId = UserId.Create(query.UserId);

        User? user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
        {
            return Error.NotFound("User.NotFound", "User with the id provided was not found");
        }

        return new UserDto(
            user.UserId.Value,
            user.Login,
            user.Name);
    }
}
