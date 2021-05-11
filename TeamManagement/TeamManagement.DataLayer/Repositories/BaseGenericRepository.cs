using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TeamManagement.DataLayer.Data;
using TeamManagement.DataLayer.Domain.Interdaces;
using TeamManagement.DataLayer.Repositories.Interfaces;

namespace TeamManagement.DataLayer.Repositories
{
    public class BaseGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IIdentificated
    {
        private AppDbContext _context;
        private DbSet<TEntity> _dbSet;

        public BaseGenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<bool> DeleteAsync(TEntity entityToDelete)
        {
            _dbSet.Remove(entityToDelete);

            var deleted = await SaveAsync();
            return deleted > 0;
        }

        public async Task<bool> DeleteAsync(object id)
        {
            TEntity entityToDelete = await _dbSet.FindAsync(Guid.Parse(id.ToString()));

            if (entityToDelete == null)
            {
                return false;
            }

            var result = await DeleteAsync(entityToDelete);
            return result;
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeFunc = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeFunc != null)
            {
                query = includeFunc(query);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(object id, Func<IQueryable<TEntity>,
            IIncludableQueryable<TEntity, object>> includeFunc = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includeFunc != null)
            {
                query = includeFunc(query);
            }

            return await query.FirstOrDefaultAsync(entity => entity.Id == Guid.Parse(id.ToString()));
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);

            return await SaveAsync() > 0;
        }

        public async Task<bool> UpdateAsync(TEntity entityToUpdate)
        {
            _dbSet.Update(entityToUpdate);

            var updated = await SaveAsync();
            return updated > 0;
        }

        private async Task<int> SaveAsync() => await _context.SaveChangesAsync();

        public async Task<IEnumerable<TResponse>> GetWithGroupByAsync<TResponse>(Func<TEntity, object> groupByFunc,
                                                                Func<IGrouping<object, TEntity>, TResponse> select,
                                                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeFunc = null,
                                                                Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeFunc != null)
            {
                query = includeFunc(query);
            }

            var grouping = (await query.ToListAsync()).GroupBy(groupByFunc);

            var selected = grouping.Select(select);

            return selected;
        }

        public async Task<IEnumerable<TSelectObject>> GetWithSelectAsync<TSelectObject>(Func<TEntity, TSelectObject> select,
                                                                                        Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = _dbSet.Where(filter);
            }

            return (await _dbSet.ToListAsync()).Select(select);
        }

    }
}
