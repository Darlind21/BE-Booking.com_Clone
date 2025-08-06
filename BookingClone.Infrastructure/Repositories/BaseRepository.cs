using BookingClone.Application.Interfaces.Repositories;
using BookingClone.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookingClone.Infrastructure.Repositories
{
    public class BaseRepository<T>(BookingDbContext context) : IBaseRepository<T> where T : class
    {
        public virtual async Task<bool> AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            return await context.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> DeleteByIdAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);

            if (entity != null)
            {
                context.Set<T>().Remove(entity);
                return await context.SaveChangesAsync() > 0;
            }

            return false;
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
            //Func<T, bool> is a delegate (a reference to a funcion) that takes a parameter of type T and returns a bool
            //Basically a function that takes a T (e.g. a User) and returns true/false based on some condition
            //The Expression<> wrapper is what makes it possible for EF Core to translate the lambda expression in SQL
            //So predicate parameter is a lambda expression which in itself takes a T and returns a bool
            //In plain english "Its just a condition you pass in so the method knows what to check for"
        {
            return await context.Set<T>().AnyAsync(predicate);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            context.Set<T>().Update(entity);

            return await context.SaveChangesAsync() > 0;
        }
    }
}