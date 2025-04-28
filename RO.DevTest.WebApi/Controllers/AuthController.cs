using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using RO.DevTest.Application.Features.Auth.Commands.LoginCommand;

namespace RO.DevTest.WebApi.Controllers;

[Route("api/auth")]
[OpenApiTags("Auth")]
public class AuthController(IMediator mediator) : Controller {
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> AuthUser(LoginCommand request) {
        LoginResponse response = await _mediator.Send(request);
        return Ok(response);
         
    }
}
