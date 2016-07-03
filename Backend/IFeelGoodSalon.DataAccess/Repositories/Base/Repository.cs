using IFeelGoodSalon.Data.Base;
using IFeelGoodSalon.Data.Repository.Base;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace IFeelGoodSalon.DataAccess.Repositories.Base
{
    public class Repository<TEntity> : IRepositoryAsync<TEntity> where TEntity : class
    {
        private readonly IAmbientDbContextLocator _dbContextLocator;

        public Repository(IAmbientDbContextLocator dbContextLocator)
        {
            if (dbContextLocator == null)
            {
                throw new ArgumentNullException("dbContextLocator");
            }

            this._dbContextLocator = dbContextLocator;
        }

        protected IFeelGoodSalonContext DbContext
        {
            get
            {
                var dbContext = _dbContextLocator.Get<IFeelGoodSalonContext>();

                if (dbContext == null)
                {
                    throw new InvalidOperationException("No ambient DbContext of type IFeelGoodSalonContext found. This means that this repository method has been called outside of the scope of a DbContextScope. A repository must only be accessed within the scope of a DbContextScope, which takes care of creating the DbContext instances that the repositories need and making them available as ambient contexts. This is what ensures that, for any given DbContext-derived type, the same instance is used throughout the duration of a business transaction. To fix this issue, use IDbContextScopeFactory in your top-level business logic service method to create a DbContextScope that wraps the entire business transaction that your service method implements. Then access this repository within that scope. Refer to the comments in the IDbContextScope.cs file for more details.");
                }

                return dbContext;
            }
        }

        #region Query

        public virtual TEntity Find(params object[] keyValues)
        {
            return DbContext.Set<TEntity>().Find(keyValues);
        }

        public virtual async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await DbContext.Set<TEntity>().FindAsync(keyValues);
        }

        public virtual async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return await DbContext.Set<TEntity>().FindAsync(cancellationToken, keyValues);
        }

        public IQueryable<TEntity> Queryable()
        {
            return DbContext.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters)
        {
            return DbContext.Set<TEntity>().SqlQuery(query, parameters).AsQueryable();
        }

        #endregion

        #region Non-Query

        public virtual bool Insert(TEntity entity)
        {
            var inserted = DbContext.Set<TEntity>().Add(entity);

            return inserted != null;
        }

        public virtual bool InsertRange(IEnumerable<TEntity> entities)
        {
            var range = DbContext.Set<TEntity>().AddRange(entities);

            return range.Count() > 0;
        }

        public virtual void Update(TEntity entity)
        {
            DbContext.Set<TEntity>().Attach(entity);
        }

        public virtual bool Delete(object id)
        {
            var entity = Find(id);

            if (entity == null)
            {
                return false;
            }

            return Delete(entity);
        }

        public virtual bool Delete(TEntity entity)
        {
            var deleted = DbContext.Set<TEntity>().Remove(entity);

            return deleted != null;
        }

        public virtual async Task<bool> DeleteAsync(params object[] keyValues)
        {
            return await DeleteAsync(CancellationToken.None, keyValues);
        }

        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            var entity = await FindAsync(cancellationToken, keyValues);

            if (entity == null)
            {
                return false;
            }

            var deleted = DbContext.Set<TEntity>().Remove(entity);

            return deleted != null;
        }

        #endregion

        internal IQueryable<TEntity> Select(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }

            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return query;
        }

        internal async Task<IEnumerable<TEntity>> SelectAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            return await Select(filter, orderBy, includes, page, pageSize).ToListAsync();
        }
    }
}
