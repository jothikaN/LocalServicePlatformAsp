
using LocalServicePlatform.Application.Contracts.Presistance;
using LocalServicePlatform.Domain.Common;
using LocalServicePlatform.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LocalServicePlatform.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        protected readonly ApplicationDbContext _dbContext;
    
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
       
        }
        public async Task Create(T entity)
        {
            await _dbContext.AddAsync(entity);
        }
        public async Task Delete(T entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> Get(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public Task<bool> IsRecordExixts(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate).AnyAsync();
        }

        public async Task<bool> IsRecordExsits(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AnyAsync(predicate);
        }
        public IQueryable<T> Query(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate);
        }
        public IQueryable<T> Query()
        {
            return _dbContext.Set<T>().AsQueryable();
        }
        IEnumerable<T> IGenericRepository<T>.Query(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate);
        }

        IEnumerable<T> IGenericRepository<T>.Query()
        {
            return _dbContext.Set<T>().AsNoTracking();
        }


    }
}
