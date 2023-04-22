using Microsoft.EntityFrameworkCore;
using PRAS.Contracts.Repositories;
using System.Linq.Expressions;

namespace PRAS.Persistence.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private AppDbContext _context;
        public RepositoryBase(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(T entit) =>
            await _context.Set<T>().AddAsync(entit);

        public void Delete(T entity) =>
            _context.Set<T>().Remove(entity);

        public IQueryable<T> GetAll(bool trackChanges) =>
            trackChanges ?
                _context.Set<T>() :
                _context.Set<T>().AsNoTracking();

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            trackChanges ?
                _context.Set<T>().Where(expression) :
                _context.Set<T>().Where(expression).AsNoTracking();
    }
}
