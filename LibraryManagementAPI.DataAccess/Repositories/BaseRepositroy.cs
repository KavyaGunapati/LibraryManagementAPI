
using LibraryManagementAPI.DataAccess.Context;
using LibraryManagementAPI.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryManagementAPI.DataAccess.Repositories
{
    public class BaseRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly LibraryDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(LibraryDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
            else
            {
                throw new KeyNotFoundException($"entity with {id} not found");
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
