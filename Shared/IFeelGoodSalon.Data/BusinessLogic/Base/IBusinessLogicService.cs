using System;
using System.Collections.Generic;
using System.Linq;

namespace IFeelGoodSalon.Data.BusinessLogic.Base
{
    public interface IBusinessLogicService<TEntity> : IDisposable where TEntity : class
    {
        TEntity Find(params object[] keyValues);

        bool Insert(TEntity entity);
        bool InsertRange(IEnumerable<TEntity> entities);

        bool Delete(object id);
        bool Delete(TEntity entity);

        bool Update(TEntity entity);

        //IQueryFluent<TEntity> Query();
        //IQueryFluent<TEntity> Query(IQueryLogic<TEntity> queryObject);
        //IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query);

        IQueryable<TEntity> Queryable();
        IQueryable<TEntity> SelectQuery(string query, params object[] parameters);
    }
}
