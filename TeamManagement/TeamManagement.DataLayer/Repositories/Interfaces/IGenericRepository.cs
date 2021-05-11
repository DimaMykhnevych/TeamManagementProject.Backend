using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TeamManagement.DataLayer.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<bool> DeleteAsync(object id);
        Task<bool> DeleteAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeFunc = null);
        Task<TEntity> GetByIdAsync(object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeFunc = null);
        Task<bool> CreateAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entityToUpdate);
        Task<IEnumerable<TResponse>> GetWithGroupByAsync<TResponse>(Func<TEntity, object> groupByFunc,
                                                                Func<IGrouping<object, TEntity>, TResponse> select,
                                                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeFunc = null,
                                                                Expression<Func<TEntity, bool>> filter = null);
        Task<IEnumerable<TSelectObject>> GetWithSelectAsync<TSelectObject>(Func<TEntity, TSelectObject> select,
                                                                           Expression<Func<TEntity, bool>> filter = null);
    }
}
