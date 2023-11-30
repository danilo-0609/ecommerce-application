using Ecommerce.BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;

namespace Ecommerce.UserAccess.Application.UserRegistrations.ConfirmUserRegistration;

public sealed record ConfirmUserRegistrationCommand(
    Guid UserRegistrationId) : IQueryRequest<ErrorOr<Unit>>;
