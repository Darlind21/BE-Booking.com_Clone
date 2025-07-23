using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class //we specify that T must be a class type because we want to work with entities and not with value types (like int, double, etc.)
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<bool> DeleteByIdAsync(Guid id);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
