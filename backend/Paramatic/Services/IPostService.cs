using Paramatic.Models;
using Microsoft.AspNetCore.Http;

namespace Paramatic.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post?> GetPostByIdAsync(string id);
        Task<Post> CreatePostAsync(Post post, IFormFile? videoContent);
        Task<Post> UpdatePostAsync(Post post);
        Task DeletePostAsync(string id);
    }
} 