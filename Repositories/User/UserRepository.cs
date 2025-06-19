using Microsoft.EntityFrameworkCore;
using TodoServices.Data;
using TodoServices.Models;

namespace TodoServices.Repositories.User
{
    public class UserRepository(TodoDbContext context) : IUserRepository
    {
        public async Task<UserModel?> GetByUsernameAsync(string username) => await context.Users.FirstOrDefaultAsync(u => u.Username == username);
        public async Task<bool> UserExistsAsync(string username, string email) => await context.Users.AnyAsync(u => u.Username == username || u.Email == email);
        public async Task<UserModel> AddAsync(UserModel user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }
    }
}
