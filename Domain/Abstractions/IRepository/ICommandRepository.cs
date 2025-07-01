using Domain.Models;

namespace Domain.Abstractions.IRepository
{
    public interface ICommandRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveAsync();
    }
}
