using Paramatic.Models;

public interface IPostRepository
{
    Task<Post?> GetByIdAsync(string id);
    Task<IEnumerable<Post>> GetAllAsync();
    Task CreateAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(string id);
    
    // Post-specific methods can be added here
    Task<IEnumerable<Post>> GetPostsByCreatorIdAsync(string creatorId);
    Task<IEnumerable<Post>> GetRecentPostsAsync(int limit);
} 