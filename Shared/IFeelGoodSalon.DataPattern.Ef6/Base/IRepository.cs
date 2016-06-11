using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IFeelGoodSalon.DataPattern.Ef6.Base
{
    public interface IRepository<TEntity> where TEntity : class, IObservableEntity
    {
        void Delete(object id);
        void Delete(TEntity entity);

        TEntity Find(params object[] keyValues);

        IRepository<T> GetRepository<T>() where T : class, IObservableEntity;

        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void InsertGraphRange(IEnumerable<TEntity> entities);
        void InsertOrUpdateGraph(TEntity entity);
        

        IQueryFluent<TEntity> Query(IQueryLogic<TEntity> queryObject);
        IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query);
        IQueryFluent<TEntity> Query();

        IQueryable<TEntity> Queryable();
        IQueryable<TEntity> SelectQuery(string query, params object[] parameters);

        void Update(TEntity entity);
    }
}
