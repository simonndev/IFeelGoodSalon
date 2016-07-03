using IFeelGoodSalon.Data.BusinessLogic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using IFeelGoodSalon.Data.Repository.Base;
using IFeelGoodSalon.Data.Base;

namespace IFeelGoodSalon.BusinessLogic.Base
{
    public abstract class BusinessLogicService<TEntity> : IBusinessLogicServiceAsync<TEntity> where TEntity : class
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepositoryAsync<TEntity> _repository;

        private bool _isDisposed;

        protected BusinessLogicService(IDbContextScopeFactory dbContextScopeFactory, IRepositoryAsync<TEntity> repository)
        {
            if (dbContextScopeFactory == null)
            {
                throw new ArgumentNullException("dbContextScopeFactory");
            }

            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            _dbContextScopeFactory = dbContextScopeFactory;
            _repository = repository;
        }

        public virtual TEntity Find(params object[] keyValues)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var entity = _repository.Find(keyValues);
                if (entity == null)
                {
                    throw new ArgumentException(String.Format("Couldn't find any record with this ID.", keyValues));
                }

                return entity;
            }
        }

        public virtual async Task<TEntity> FindAsync(params object[] keyValues)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var entity = await _repository.FindAsync(keyValues).ConfigureAwait(false);
                if (entity == null)
                {
                    throw new ArgumentException(String.Format("Couldn't find any record with this ID.", keyValues));
                }

                return entity;
            }
        }

        public virtual async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var entity = await _repository.FindAsync(cancellationToken, keyValues).ConfigureAwait(false);
                if (entity == null)
                {
                    throw new ArgumentException(String.Format("Couldn't find any record with this ID.", keyValues));
                }

                return entity;
            }
        }

        public virtual IQueryable<TEntity> Queryable()
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                return _repository.Queryable();
            }
        }

        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                return _repository.SelectQuery(query, parameters);
            }
        }

        public virtual bool Insert(TEntity entity)
        {
            bool succeed = false;

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                succeed = _repository.Insert(entity);
                if (succeed)
                {
                    dbContextScope.SaveChanges();
                }

                return succeed;
            }
        }

        public virtual async Task<bool> InsertAsync(TEntity entity)
        {
            return await InsertAsync(CancellationToken.None, entity);
        }

        public virtual async Task<bool> InsertAsync(CancellationToken cancellationToken, TEntity entity)
        {
            bool succeed = false;

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                succeed = _repository.Insert(entity);

                if (succeed)
                {
                    await dbContextScope.SaveChangesAsync();
                }

                return succeed;
            }
        }

        public virtual bool InsertRange(IEnumerable<TEntity> entities)
        {
            bool succeed = false;

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                succeed = _repository.InsertRange(entities);
                if (succeed)
                {
                    dbContextScope.SaveChanges();
                }

                return succeed;
            }
        }

        public virtual async Task<bool> InsertRangeAsync(IEnumerable<TEntity> entities)
        {
            return await InsertRangeAsync(CancellationToken.None, entities);
        }

        public virtual async Task<bool> InsertRangeAsync(CancellationToken cancellationToken, IEnumerable<TEntity> entities)
        {
            bool succeed = false;

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                succeed = _repository.InsertRange(entities);

                if (succeed)
                {
                    await dbContextScope.SaveChangesAsync();
                }

                return succeed;
            }
        }

        public virtual bool Update(TEntity entity)
        {
            bool succeed = false;

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                _repository.Update(entity);

                succeed = dbContextScope.SaveChanges() > 0;

                return succeed;
            }
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            bool succeed = false;

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                _repository.Update(entity);

                succeed = await dbContextScope.SaveChangesAsync() > 0;

                return succeed;
            }
        }

        public virtual async Task<bool> UpdateAsync(CancellationToken cancellationToken, TEntity entity)
        {
            bool succeed = false;

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                _repository.Update(entity);

                succeed = await dbContextScope.SaveChangesAsync() > 0;

                return succeed;
            }
        }

        public virtual bool Delete(TEntity entity)
        {
            bool succeed = false;

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                succeed = _repository.Delete(entity);
                if (succeed)
                {
                    dbContextScope.SaveChanges();
                }

                return succeed;
            }
        }

        public virtual bool Delete(object id)
        {
            bool succeed = false;

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                succeed = _repository.Delete(id);
                if (succeed)
                {
                    dbContextScope.SaveChanges();
                }

                return succeed;
            }
        }

        public virtual async Task<bool> DeleteAsync(params object[] keyValues)
        {
            bool succeed = false;

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                succeed = await _repository.DeleteAsync(keyValues).ConfigureAwait(false);
                if (succeed)
                {
                    dbContextScope.SaveChanges();
                }

                return succeed;
            }
        }

        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            bool succeed = false;

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                succeed = await _repository.DeleteAsync(cancellationToken, keyValues).ConfigureAwait(false);
                if (succeed)
                {
                    dbContextScope.SaveChanges();
                }

                return succeed;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            if (disposing)
            {
                // free other managed objects that implement
                // IDisposable only

                this._dbContextScopeFactory.SuppressAmbientContext().Dispose();
            }

            // release any unmanaged objects
            // set the object references to null

            _isDisposed = true;
        }
    }
}
