using Microsoft.EntityFrameworkCore;
using SlotGameServer.Application.Contracts.Persistence;
using SlotGameServer.Persistence.Context;
using System.Linq.Expressions;


namespace SlotGameServer.Persistence.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SlotGameServerDbContext _dbContext;
        private readonly DbSet<T> _db;

        public GenericRepository(SlotGameServerDbContext dbContext)
        {
            _dbContext = dbContext;
            _db = _dbContext.Set<T>();
        }

        public async Task<T> Add(T entity)
        {
            await _db.AddAsync(entity);
            return entity;
        }

        public async ValueTask AddRange(IEnumerable<T> entities)
        {
            await _db.AddRangeAsync(entities);
        }

        public async ValueTask Delete(int id)
        {
            var entity = await _db.FindAsync(id) ?? default!;
            _db.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await Get(id);
            return entity != null;
        }

        public async Task<T> Get(int id)
        {
            return await _db.FindAsync(id) ?? default!;
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _db.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _db.Where(predicate).ToListAsync(cancellationToken);
        }

        public void Update(T entity)
        {
            _db.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task<T?> GetSingleAsync(Expression<Func<T, bool>> @where, CancellationToken cancellationToken)
        {
            return await _db.FirstOrDefaultAsync(where, cancellationToken);
        }


        public IQueryable<T> GetAllQueryable()
        {
            return _db.AsQueryable();
        }

        public async Task<T> GetSingleWithIncludesAsync(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes)
        {
            var query = _db.AsQueryable();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(where, cancellationToken) ?? default!;
        }
    }
}
