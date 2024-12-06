using Amazon.DynamoDBv2.DataModel;

namespace Paramatic.Repositories
{
    public class DynamoDBRepository<T> : IDynamoDBRepository<T> where T : class
    {
        private readonly IDynamoDBContext _context;

        public DynamoDBRepository(IDynamoDBContext context)
        {
            _context = context;
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            return await _context.LoadAsync<T>(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.ScanAsync<T>(new List<ScanCondition>()).GetRemainingAsync();
        }

        public async Task CreateAsync(T entity)
        {
            await _context.SaveAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            await _context.SaveAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await _context.DeleteAsync<T>(id);
        }
    }
}