using System;
using System.Linq.Expressions;

namespace IFeelGoodSalon.Data.Query.Base
{
    public interface IQueryLogic<TEntity> where TEntity : class
    {
        Expression<Func<TEntity, bool>> And(Expression<Func<TEntity, bool>> query);
        Expression<Func<TEntity, bool>> And(IQueryLogic<TEntity> queryObject);

        Expression<Func<TEntity, bool>> Or(Expression<Func<TEntity, bool>> query);
        Expression<Func<TEntity, bool>> Or(IQueryLogic<TEntity> queryObject);

        Expression<Func<TEntity, bool>> Query();
    }
}
