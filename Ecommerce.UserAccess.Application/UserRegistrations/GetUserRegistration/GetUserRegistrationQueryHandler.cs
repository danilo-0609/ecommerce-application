using Ecommerce.BuildingBlocks.Application.Queries;
using Ecommerce.UserAccess.Domain.UserRegistrations;
using ErrorOr;

namespace Ecommerce.UserAccess.Application.UserRegistrations.GetUserRegistration;

internal sealed class GetUserRegistrationQueryHandler
    : IQueryRequestHandler<GetUserRegistrationQuery, ErrorOr<UserRegistrationDto>>
{
    private readonly IUserRegistrationRepository _userRegistrationRepository;

    public GetUserRegistrationQueryHandler(IUserRegistrationRepository userRegistrationRepository)
    {
        _userRegistrationRepository = userRegistrationRepository;
    }

    public async Task<ErrorOr<UserRegistrationDto>> Handle(GetUserRegistrationQuery query, CancellationToken cancellationToken)
    {
        UserRegistrationId userRegistrationId = UserRegistrationId.Create(query.UserRegistrationId);

        UserRegistration? userRegistration = await _userRegistrationRepository.GetByIdAsync(userRegistrationId);
    
        if (userRegistration is null)
        {
            return Error.NotFound("UserRegistration.NotFound", "User registration was not found");
        }

        return new UserRegistrationDto(userRegistrationId.Value,
            userRegistration.Login,
            userRegistration.Email,
            userRegistration.FirstName,
            userRegistration.LastName,
            userRegistration.Name,
            userRegistration.Status.Value);
    }
}
