

using TodoServices.Models;

namespace TodoServices.Services.Token
{
    public interface ITokenService
    {
        string CreateToken(UserModel user);
    }
}
