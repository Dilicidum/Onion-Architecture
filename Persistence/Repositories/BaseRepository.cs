using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
namespace Persistence.Repositories
{
    public class BaseRepository<TEntity> : IBaseGenericRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationContext context;
        private DbSet<TEntity> dbSet;
        public BaseRepository(ApplicationContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        public void Delete(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public async Task DeleteByIdAsync(object id)
        {
            var entity = await dbSet.FindAsync(id);
            dbSet.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderby = null, string includingProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includingProperties != null)
            {
                foreach(var includeProperty in includingProperties.Split(new char[] { ','}
                ,StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if(orderby != null)
            {
                return (await orderby(query).ToListAsync());
            }
            else
            {
                return await query.ToListAsync();
            }

        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            var entity = await dbSet.FindAsync(id);
            return entity;
        }

        public async Task InsertAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity, params Expression<Func<TEntity, object>>[] includeProperties)
        {

            //this.context.Set<TEntity>().Update(entity);
            dbSet.Update(entity);
            //dbSet.Attach(entity);
            
            //context.Entry(entity).CurrentValues.SetValues(entity);
            //var entry = context.Entry(entity);
            //entry.State = EntityState.Modified;
            //context.Entry(entity).State = EntityState.Modified;
            
        }
    }
}
