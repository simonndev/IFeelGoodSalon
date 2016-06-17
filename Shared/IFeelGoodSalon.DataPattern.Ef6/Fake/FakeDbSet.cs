using IFeelGoodSalon.DataPattern.Ef6.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IFeelGoodSalon.DataPattern.Ef6.Fake
{
    public abstract class FakeDbSet<TEntity>
        : DbSet<TEntity>, IDbSet<TEntity> where TEntity : class, IObservableEntity, new()
    {
        #region Fields
        private readonly ObservableCollection<TEntity> _items;
        private readonly IQueryable _query;
        #endregion

        protected FakeDbSet()
        {
            this._items = new ObservableCollection<TEntity>();
            this._query = this._items.AsQueryable<TEntity>();
        }

        #region Properties

        public Type ElementType
        {
            get
            {
                return this._query.ElementType;
            }
        }

        public Expression Expression
        {
            get
            {
                return this._query.Expression;
            }
        }

        public override ObservableCollection<TEntity> Local
        {
            get
            {
                return this._items;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return this._query.Provider;
            }
        }

        #endregion

        public override TEntity Add(TEntity entity)
        {
            this._items.Add(entity);
            return entity;
        }

        public override TEntity Remove(TEntity entity)
        {
            this._items.Remove(entity);
            return entity;
        }

        public override TEntity Attach(TEntity entity)
        {
            switch (entity.ModelState)
            {
                case EntityModelState.Unchanged:
                case EntityModelState.Added:
                    this._items.Add(entity);
                    break;
                case EntityModelState.Modified:
                    this._items.Remove(entity);
                    this._items.Add(entity);
                    break;
                case EntityModelState.Deleted:
                    this._items.Remove(entity);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return entity;
        }

        public override TEntity Create()
        {
            return new TEntity();
        }

        public override TDerivedEntity Create<TDerivedEntity>()
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return this._items.GetEnumerator();
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._items.GetEnumerator();
        }
    }
}
