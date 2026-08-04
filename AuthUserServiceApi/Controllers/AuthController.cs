using AuthUserServiceApplication.DTOs.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthUserServiceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _mediator.Send(
                new LoginCommand(
                    request.Email,
                    request.Password));

            return Ok(result);
        }
    }
}
