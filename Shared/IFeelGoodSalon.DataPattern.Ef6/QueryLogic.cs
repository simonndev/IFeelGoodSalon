using IFeelGoodSalon.DataPattern.Ef6.Base;
using LinqKit;
using System;
using System.Linq.Expressions;

namespace IFeelGoodSalon.DataPattern.Ef6
{
    public abstract class QueryLogic<TEntity> : IQueryLogic<TEntity> where TEntity : class, IObservableEntity, new()
    {
        private Expression<Func<TEntity, bool>> _query;

        public virtual Expression<Func<TEntity, bool>> Query()
        {
            return _query;
        }

        public Expression<Func<TEntity, bool>> And(Expression<Func<TEntity, bool>> query)
        {
            return _query = _query == null ? query : _query.And(query.Expand());
        }

        public Expression<Func<TEntity, bool>> Or(Expression<Func<TEntity, bool>> query)
        {
            return _query = _query == null ? query : _query.Or(query.Expand());
        }

        public Expression<Func<TEntity, bool>> And(IQueryLogic<TEntity> queryObject)
        {
            return And(queryObject.Query());
        }

        public Expression<Func<TEntity, bool>> Or(IQueryLogic<TEntity> queryObject)
        {
            return Or(queryObject.Query());
        }
    }
}
