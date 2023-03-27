using Ordering.Domain.Common;
using System.Linq.Expressions;

namespace Ordering.Application.Interfaces.Persistence
{
    public interface IRepository<T> where T : ITrackable
    {
        Task<IReadOnlyList<T>> GetAll();
        Task<IReadOnlyList<T>> Get(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> Get(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeString = null, bool disableTracking = true);
        Task<IReadOnlyList<T>> Get(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includes = null, bool disableTracking = true);

        Task<T> GetById(long id);
        Task<T> Add(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
