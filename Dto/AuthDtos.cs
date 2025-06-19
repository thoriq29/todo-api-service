using System.ComponentModel.DataAnnotations;

namespace TodoServices.Dto
{
    public class AuthDtos
    {
        public record RegisterDto([Required] string Username, [Required][EmailAddress] string Email, [Required] string Password);
        public record LoginDto([Required] string Username, [Required] string Password);
        public record LoginResponseDto(string Message, string Token);
    }
}
