using EShop.API.Data;
using EShop.API.Models;
using EShop.API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace EShop.API.Repository
{
    public class CommandRepository<T>(AppDbContext _context) : ICommandRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _dbset = _context.Set<T>();

        public async Task AddAsync(T entity)
        {
            await _dbset.AddAsync(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbset.Remove(entity);
            await SaveAsync();
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbset.Update(entity);
            await SaveAsync();
        }
    }
}