using EShop.API.Data;
using EShop.API.Models;
using EShop.API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EShop.API.Repository
{
    public class QueryRepository<T>(AppDbContext _context) : IQueryRepository<T> where T : BaseEntity
    {
        internal DbSet<T> dbSet = _context.Set<T>();

        public IQueryable<T> GetAll(Expression<Func<T, bool>>? filter = null,Func<IQueryable<T> ,IQueryable<T>>? include = null,CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = dbSet.AsNoTracking();
            if (include is not null)
                query = include(query);
            if (filter is not null) 
                query = query.Where(filter);
            return query;
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>>? filter, Func<IQueryable<T>, IQueryable<T>>? include = null,CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = dbSet.AsNoTracking();
            if (include is not null)
                query = include(query);
            if (filter is not null) 
                query = query.Where(filter);

            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
