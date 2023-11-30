using Ecommerce.API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Modules.Catalog;

[Route("api/[controller]")]
[ApiController]
public sealed class SellerController : ApiController
{
    private readonly ISender _sender;

    public SellerController(ISender sender)
    {
        _sender = sender;
    }
}
