using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace hometask.Data.Repositories.Impl {
    public class Repository<T> : IRepository<T> where T : class {

        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(DbContext context) {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
            => await _dbSet.FindAsync(id);

        public virtual async Task<IEnumerable<T>> GetAllAsync()
            => await _dbSet.ToListAsync();

        public virtual async Task<T> AddAsync(T entity) {
            return (await _dbSet.AddAsync(entity)).Entity;
        }

        public virtual void Update(T entity)
            => _dbSet.Update(entity);

        public virtual void Remove(T entity)
            => _dbSet.Remove(entity);

        public async Task SaveChangesAsync()
            => await _dbContext.SaveChangesAsync();

        public async Task<IDbContextTransaction> BeginTransactionAsync()
            => await _dbContext.Database.BeginTransactionAsync();

        public async Task<int> CountAsync()
            => await _dbSet.CountAsync();

        public async Task<bool> ExistsAsync(Guid id) {
            var parameter = Expression.Parameter(typeof(T), "e");
            var property = Expression.PropertyOrField(parameter, "Id");
            var value = Expression.Constant(id);
            var equal = Expression.Equal(property, value);
            var lambda = Expression.Lambda<Func<T, bool>>(equal, parameter);

            return await _dbSet.AnyAsync(lambda);
        }

    }
}
