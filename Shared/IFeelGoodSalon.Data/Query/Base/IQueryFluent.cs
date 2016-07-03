using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IFeelGoodSalon.Data.Query.Base
{
    public interface IQueryFluent<TEntity> where TEntity : class
    {
        IQueryFluent<TEntity> Include(Expression<Func<TEntity, object>> expression);
        IQueryFluent<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);

        IEnumerable<TEntity> Select();
        IEnumerable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector = null);
        IEnumerable<TEntity> SelectPage(int page, int pageSize, out int totalCount);
        
        IQueryable<TEntity> SqlQuery(string query, params object[] parameters);
    }
}
