using Ecommerce.API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Modules.Catalog;

[Route("api/[controller]")]
[ApiController]
public sealed class ProductController : ApiController
{
    private readonly ISender _sender;

    public ProductController(ISender sender)
    {
        _sender = sender;
    }


}
