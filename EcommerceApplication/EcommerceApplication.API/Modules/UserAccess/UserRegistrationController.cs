using Ecommerce.API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Ecommerce.UserAccess.Application.UserRegistrations.RegisterNewUser;
using ErrorOr;
using Ecommerce.UserAccess.Application.UserRegistrations.ConfirmUserRegistration;

namespace Ecommerce.API.Modules.UserAccess;

[Route("api/userAccess/[controller]")]
[ApiController]
public class UserRegistrationController : ApiController
{
    private readonly ISender _sender;

    public UserRegistrationController(ISender sender)
    {
        _sender = sender;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterNewUser(RegisterNewUserRequest registerNewUserRequest)
    {
        ErrorOr<Guid> result = await _sender.Send(new RegisterNewUserCommand(
            registerNewUserRequest.Login,
            registerNewUserRequest.Password,
            registerNewUserRequest.Email,
            registerNewUserRequest.FirstName,
            registerNewUserRequest.LastName));

        return result.Match(
            userRegisteredGuid => Created(nameof(userRegisteredGuid), userRegisteredGuid),
            errors => Problem(errors));
    }

    [AllowAnonymous]
    [HttpPatch("{userRegistrationId}/confirm")]
    public async Task<IActionResult> ConfirmRegistration(Guid userRegistrationId)
    {
        ErrorOr<Unit> result = await _sender
            .Send(new ConfirmUserRegistrationCommand(userRegistrationId));

        return result.Match(
            confirmed => NoContent(),
            errors => Problem(errors));
    }
}
