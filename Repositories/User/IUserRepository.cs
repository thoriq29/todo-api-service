using TodoServices.Models;

namespace TodoServices.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel?> GetByUsernameAsync(string username);
        Task<bool> UserExistsAsync(string username, string email);
        Task<UserModel> AddAsync(UserModel user);
    }
}