using Application.Enums;
using Application.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected Order2ServeDbContext _dbContext;
        protected ILog _logger;

        public BaseRepository(Order2ServeDbContext dbContext, ILog logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task CreateAsync(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            _dbContext.Set<T>().Add(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);        
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            _dbContext.Set<T>().Remove(entity);
            await SaveAsync();
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return  await _dbContext.Set<T>().FindAsync(id);         
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            _dbContext.Set<T>().Update(entity);
            await SaveAsync();
        }

        protected async Task<bool> SaveAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _logger.Log(ex, LogLevel.Error);
                return false;
            }
        }
    }
}
