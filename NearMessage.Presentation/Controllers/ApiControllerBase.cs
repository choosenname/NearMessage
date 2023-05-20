using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender Sender;
    protected ApiController(ISender sender)
    {
        Sender = sender;
    }
}