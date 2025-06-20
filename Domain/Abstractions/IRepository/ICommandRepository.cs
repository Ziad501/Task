using EShop.API.Models;

namespace EShop.API.Repository.IRepository
{
    public interface ICommandRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task SaveAsync();
    }
}
