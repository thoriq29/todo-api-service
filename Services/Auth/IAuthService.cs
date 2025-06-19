using TodoServices.Dto;
using static TodoServices.Dto.AuthDtos;

namespace TodoServices.Services.Auth
{
    public interface IAuthService
    {
        Task<IResult> RegisterAsync(RegisterDto dto);
        Task<IResult> LoginAsync(LoginDto dto);
    }
}
