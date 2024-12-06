using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Paramatic.Models;

namespace Paramatic.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDynamoDBContext _context;

        public UserRepository(IDynamoDBContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _context.LoadAsync<User>(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.ScanAsync<User>(new List<ScanCondition>()).GetRemainingAsync();
        }

        public async Task CreateAsync(User user)
        {
            await _context.SaveAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            await _context.SaveAsync(user);
        }

        public async Task DeleteAsync(string id)
        {
            await _context.DeleteAsync<User>(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var config = new DynamoDBOperationConfig
            {
                IndexName = "EmailIndex"
            };
            
            var results = await _context.QueryAsync<User>(email, config).GetRemainingAsync();
            return results.FirstOrDefault();
        }

        public async Task<User?> GetByUsernameAsync(string username) {
            return await _context.LoadAsync<User>(username);
        }
    }
}