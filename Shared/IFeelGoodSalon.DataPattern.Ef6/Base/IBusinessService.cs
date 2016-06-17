using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace IFeelGoodSalon.DataPattern.Ef6.Base
{
    public interface IBusinessService<TEntity> where TEntity : class, IObservableEntity, new()
    {
        void Delete(object id);
        void Delete(TEntity entity);

        Task<bool> DeleteAsync(params object[] keyValues);
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);

        TEntity Find(params object[] keyValues);

        Task<TEntity> FindAsync(params object[] keyValues);
        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);

        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);

        void InsertGraphRange(IEnumerable<TEntity> entities);
        void InsertOrUpdateGraph(TEntity entity);
        
        IQueryFluent<TEntity> Query();
        IQueryFluent<TEntity> Query(IQueryLogic<TEntity> queryObject);
        IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query);

        IQueryable<TEntity> Queryable();

        IQueryable<TEntity> SelectQuery(string query, params object[] parameters);

        void Update(TEntity entity);
    }
}
