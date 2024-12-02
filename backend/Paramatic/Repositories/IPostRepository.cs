using Paramatic.Models;

public interface IPostRepository
{
    Task<Post?> GetPostByIdAsync(string id);
    Task<IEnumerable<Post>> GetAllPostsAsync();
    Task CreatePostAsync(Post post);
    Task UpdatePostAsync(Post post);    Task DeletePostAsync(string id);
    
    // Post-specific methods can be added here
    Task<IEnumerable<Post>> GetPostsByCreatorIdAsync(string creatorId);
    Task<IEnumerable<Post>> GetRecentPostsAsync(int limit);
} 