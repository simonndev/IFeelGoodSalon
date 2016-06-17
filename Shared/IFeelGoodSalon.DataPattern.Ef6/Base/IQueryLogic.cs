using System;
using System.Linq.Expressions;

namespace IFeelGoodSalon.DataPattern.Ef6.Base
{
    public interface IQueryLogic<TEntity> where TEntity : class, IObservableEntity, new()
    {
        Expression<Func<TEntity, bool>> And(Expression<Func<TEntity, bool>> query);
        Expression<Func<TEntity, bool>> And(IQueryLogic<TEntity> queryObject);

        Expression<Func<TEntity, bool>> Or(Expression<Func<TEntity, bool>> query);
        Expression<Func<TEntity, bool>> Or(IQueryLogic<TEntity> queryObject);

        Expression<Func<TEntity, bool>> Query();
    }
}
