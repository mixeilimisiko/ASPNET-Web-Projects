using Microsoft.EntityFrameworkCore;
using System.Linq;
using ToDo.Domain;


namespace ToDo.Infrastructure
{// ak sheidzleboda genericis magivrad IEntity-s gamokeneba magram ikos ase ufro zogadi
    public class BaseRepository<T> where T : class
    {
        protected readonly DbContext _context;

        protected readonly DbSet<T> _dbSet;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        
        public virtual async Task<IEnumerable <T>> ReadAllAsync(CancellationToken token) 
        {
            return await _dbSet.ToListAsync(token);
        }

        public virtual async Task<T> ReadAsync(object?[]? keyValues, CancellationToken token)
        {
            return await _dbSet.FindAsync(keyValues, token);
        }

        public virtual async Task CreateAsync(T entity, CancellationToken token)
        {
            await _dbSet.AddAsync(entity, token);
            await _context.SaveChangesAsync(token);
        }

        public virtual async Task UpdateAsync(T entity, CancellationToken token)
        {
            if (entity == null)
                return;

            _dbSet.Update(entity);
            await _context.SaveChangesAsync(token);
        }

        public virtual async Task DeleteAsync(object?[]? keyValues, CancellationToken token)
        {
            var entity = await ReadAsync(keyValues, token);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(token);
        }

        public virtual async Task DeleteAsync(T entity, CancellationToken token)
        {
            if (entity == null)
                return;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(token);
        }

    }
}

