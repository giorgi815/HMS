using HMS.Application.Contracts.Presistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters;

namespace HMS.Infrastructure.Presistence
{
    public class RepositoryBase<T, Tcontext> : IRepositoryBase<T, Tcontext>
        where T : class
        where Tcontext : DbContext
    {

        private readonly Tcontext _context;
        private readonly DbSet<T> _dbSet;

        public RepositoryBase(Tcontext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<(IEnumerable<T> Items, int TotalCount)> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            int? pageNumber = null,
            int? pageSize = null,
            Expression<Func<T, object>> orderBy = null,
            bool ascending = true,
            CancellationToken cancellationToken = default,
            bool tracikng = true,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (!tracikng)
                query = query.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
                ;
            }

            int totalCount = await query.CountAsync(cancellationToken);

            if (orderBy != null)
            {
                query = ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);
            }

            if (pageNumber.HasValue & pageSize.HasValue)
            {
                int page = Math.Max(pageNumber.Value, 1);
                int size = Math.Max(pageSize.Value, 1);

                query = query.Skip((page - 1) * size).Take(size);
            }

            var items = await query.ToListAsync(cancellationToken);

            return (items, totalCount);

        }
        public async Task<T?> GetAsync(
            Expression<Func<T, bool>> fillter,
            bool tracking = true,
            CancellationToken cancellationToken = default,
            Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = _dbSet;

            if (!tracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            return await query.FirstOrDefaultAsync(fillter, cancellationToken);
        }
        public async Task AddAsync(T entity, CancellationToken cancellationToken = default) => await _dbSet.AddAsync(entity, cancellationToken);
        public void Update(T entity) => _dbSet.Update(entity);
        public void Remove(T entity) => _dbSet.Remove(entity);
        public void RemoveRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);

        public async Task<int> SaveAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
        public async Task<bool> ExitAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) => await _dbSet.AnyAsync(predicate, cancellationToken);

    }
}
