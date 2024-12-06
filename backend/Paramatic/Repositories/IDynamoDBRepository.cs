using System.Collections.Generic;
using System.Threading.Tasks;

namespace Paramatic.Repositories
{
    public interface IDynamoDBRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id);
    }
}