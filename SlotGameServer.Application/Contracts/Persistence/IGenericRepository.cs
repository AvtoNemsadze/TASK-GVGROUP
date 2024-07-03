using System.Linq.Expressions;

namespace SlotGameServer.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IReadOnlyList<T>> GetAll();
        Task<T> Add(T entity);
        ValueTask AddRange(IEnumerable<T> entities);
        Task<bool> Exists(int id);
        void Update(T entity);
        ValueTask Delete(int id);
        void DeleteRange(IEnumerable<T> entities);
        IQueryable<T> GetAllQueryable();
        Task<T?> GetSingleAsync(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default);
        Task<T> GetSingleWithIncludesAsync(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes);
        Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    }
}
