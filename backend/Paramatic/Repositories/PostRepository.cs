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

        public async Task<Post?> GetByIdAsync(string id)
        {
            return await _context.LoadAsync<Post>(id);
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _context.ScanAsync<Post>(new List<ScanCondition>()).GetRemainingAsync();
        }

        public async Task CreateAsync(Post post)
        {
            await _context.SaveAsync(post);
        }

        public async Task UpdateAsync(Post post)
        {
            await _context.SaveAsync(post);
        }

        public async Task DeleteAsync(string id)
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
            var posts = await GetAllAsync();
            return posts.OrderByDescending(p => p.CreatedAt).Take(limit);
        }
    }
} 