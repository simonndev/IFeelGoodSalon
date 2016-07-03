using System.Collections.Generic;
using System.Linq;

namespace IFeelGoodSalon.Data.Repository.Base
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Find(params object[] keyValues);

        //IQueryFluent<TEntity> Query(IQueryLogic<TEntity> queryObject);
        //IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query);
        //IQueryFluent<TEntity> Query();

        IQueryable<TEntity> Queryable();
        IQueryable<TEntity> SelectQuery(string query, params object[] parameters);

        bool Insert(TEntity entity);
        bool InsertRange(IEnumerable<TEntity> entities);

        bool Delete(object id);
        bool Delete(TEntity entity);

        void Update(TEntity entity);
    }
}
