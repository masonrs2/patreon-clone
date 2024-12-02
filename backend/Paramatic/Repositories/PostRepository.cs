using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Paramatic.Models;

namespace Paramatic.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly IDynamoDBContext _context;

        public PostRepository(IDynamoDBContext context)
        {
            _context = context;
        }

        public async Task<Post?> GetPostByIdAsync(string id)
        {
            return await _context.LoadAsync<Post>(id);
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _context.ScanAsync<Post>(new List<ScanCondition>()).GetRemainingAsync();
        }

        public async Task CreatePostAsync(Post post)
        {
            await _context.SaveAsync(post);
        }

        public async Task UpdatePostAsync(Post post)
        {
            await _context.SaveAsync(post);
        }

        public async Task DeletePostAsync(string id)
        {
            await _context.DeleteAsync<Post>(id);
        }

        // Post-specific implementations
        public async Task<IEnumerable<Post>> GetPostsByCreatorIdAsync(string creatorId)
        {
            var config = new DynamoDBOperationConfig
            {
                IndexName = "CreatorIdIndex"
            };
            
            return await _context.QueryAsync<Post>(creatorId, config).GetRemainingAsync();
        }

        public async Task<IEnumerable<Post>> GetRecentPostsAsync(int limit)
        {
            // Implement post-specific logic here
            var posts = await GetAllPostsAsync();
            return posts.OrderByDescending(p => p.CreatedAt).Take(limit);
        }
    }
} 