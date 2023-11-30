using Ecommerce.API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Modules.Catalog;

[Route("api/[controller]")]
[ApiController]
public sealed class RatingController : ApiController
{
    private readonly ISender _sender;

    public RatingController(ISender sender)
    {
        _sender = sender;
    }
}
