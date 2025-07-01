using Domain.Models;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public interface IQueryRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(Expression<Func<T,bool>>? filter = null, Func<IQueryable<T>, IQueryable<T>>? include = null, CancellationToken cancellationToken = default);
        Task<T?> GetAsync(Expression<Func<T, bool>>? filter, Func<IQueryable<T>, IQueryable<T>>? include = null, CancellationToken cancellationToken = default);
    }
}
