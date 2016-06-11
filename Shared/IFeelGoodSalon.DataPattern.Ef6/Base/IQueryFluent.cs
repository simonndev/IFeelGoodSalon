using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IFeelGoodSalon.DataPattern.Ef6.Base
{
    public interface IQueryFluent<TEntity> where TEntity : class, IObservableEntity
    {
        IQueryFluent<TEntity> Include(Expression<Func<TEntity, object>> expression);
        IQueryFluent<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);

        IEnumerable<TEntity> Select();
        IEnumerable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector = null);
        IEnumerable<TEntity> SelectPage(int page, int pageSize, out int totalCount);

        Task<IEnumerable<TEntity>> SelectAsync();
        
        IQueryable<TEntity> SqlQuery(string query, params object[] parameters);
    }
}
