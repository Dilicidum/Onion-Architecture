using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;
namespace Application.Interfaces.Persistence
{
    public interface IBaseGenericRepository<TEntity>where TEntity:class
    {
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity,bool>>filter = null,
            Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderby = null,
            string includingProperties="");
        Task<TEntity> GetByIdAsync(object id);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity, params Expression<Func<TEntity, object>>[] includeProperties);
        Task DeleteByIdAsync(object id);
        void Delete(TEntity entity);
    }
}
