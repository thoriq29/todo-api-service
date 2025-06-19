using Microsoft.AspNetCore.Mvc;
using static TodoServices.Dto.AuthDtos;
using TodoServices.Services.Auth;

namespace TodoServices.Controllers
{
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IResult> Register(RegisterDto dto) => await authService.RegisterAsync(dto);

        [HttpPost("login")]
        public async Task<IResult> Login(LoginDto dto) => await authService.LoginAsync(dto);
    }
}
