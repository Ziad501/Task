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
            var local = _dbset.Local.FirstOrDefault(e => e.Id == entity.Id);
            if (local is not null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


    }
}