using TodoServices.Dto;
using TodoServices.Models;
using TodoServices.Repositories;
using TodoServices.Services;
using TodoServices.Services.Token;
using static TodoServices.Dto.AuthDtos;

namespace TodoServices.Services.Auth
{
    public class AuthService(IUserRepository userRepository, ITokenService tokenService) : IAuthService
    {
        public async Task<IResult> RegisterAsync(RegisterDto dto)
        {
            try
            {
                if (await userRepository.UserExistsAsync(dto.Username, dto.Email))
                {
                    return Results.Conflict(new ApiResponse<object?>(null, StatusCodes.Status409Conflict, "Username atau Email sudah terdaftar."));
                }

                var user = new UserModel
                {
                    Username = dto.Username,
                    Email = dto.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
                };

                var createdUser = await userRepository.AddAsync(user);

                var response = new ApiResponse<object>(
                    new { message = "Registrasi berhasil!", userId = createdUser.ID },
                    StatusCodes.Status201Created);

                return Results.Created($"/users/{createdUser.ID}", response);
            }
            catch (Exception ex)
            {
                return Results.Json(
                    new ApiResponse<object?>(null, 500, "Terjadi kesalahan saat registrasi: " + ex.Message),
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        public async Task<IResult> LoginAsync(LoginDto dto)
        {
            try
            {
                var user = await userRepository.GetByUsernameAsync(dto.Username);

                if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                {
                    return Results.Json(
                        new ApiResponse<object?>(null, 401, "Username atau password salah."),
                        statusCode: StatusCodes.Status401Unauthorized);
                }

                var token = tokenService.CreateToken(user);

                var tokenDto = new LoginResponseDto("Login berhasil!", token);
                var response = new ApiResponse<LoginResponseDto>(tokenDto, 200);

                return Results.Ok(response);
            }
            catch (Exception ex)
            {
                return Results.Json(
                    new ApiResponse<object?>(null, 500, "Terjadi kesalahan saat login: " + ex.Message),
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
