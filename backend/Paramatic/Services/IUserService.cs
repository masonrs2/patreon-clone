using Paramatic.Models;
using Microsoft.AspNetCore.Http;

namespace Paramatic.Services 
{
    public interface IUserService 
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(string id);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task DeleteUserByIdAsync(string id);
    }
}