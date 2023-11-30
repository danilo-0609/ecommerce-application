using Ecommerce.BuildingBlocks.Application.Queries;
using ErrorOr;

namespace Ecommerce.UserAccess.Application.UserRegistrations.GetUserRegistration;

public sealed record GetUserRegistrationQuery(
    Guid UserRegistrationId) : IQueryRequest<ErrorOr<UserRegistrationDto>>;
