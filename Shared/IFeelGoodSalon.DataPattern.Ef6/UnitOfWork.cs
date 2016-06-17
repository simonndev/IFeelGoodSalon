using IFeelGoodSalon.DataPattern.Ef6.Base;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace IFeelGoodSalon.DataPattern.Ef6
{
    public class UnitOfWork : IUnitOfWorkAsync
    {
        #region Private Fields

        private IObservableDbContextAsync _dbContext;
        private bool _disposed;
        private ObjectContext _objectContext;
        private DbTransaction _transaction;
        private Dictionary<string, dynamic> _repositories;

        #endregion Private Fields

        #region Constuctor

        public UnitOfWork(IObservableDbContextAsync dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Dictionary<string, dynamic>();
        }

        #endregion Constuctor/Dispose

        #region Dispose
        //https://msdn.microsoft.com/library/ms244737.aspx

        // Dispose() calls Dispose(true)
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        // NOTE: Leave out the finalizer altogether if this class doesn't 
        // own unmanaged resources itself, but leave the other methods
        // exactly as they are. 
        ~UnitOfWork()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }
        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // free other managed objects that implement
                    // IDisposable only

                    try
                    {
                        if (_objectContext != null)
                        {
                            if (_objectContext.Connection.State == ConnectionState.Open)
                            {
                                _objectContext.Connection.Close();
                            }

                            _objectContext.Dispose();
                            _objectContext = null;
                        }

                        if (_dbContext != null)
                        {
                            _dbContext.Dispose();
                            _dbContext = null;
                        }
                    }
                    catch (ObjectDisposedException)
                    {
                        // do nothing, the objectContext has already been disposed
                    }

                    if (_repositories != null)
                    {
                        _repositories = null;
                    }
                }

                _disposed = true;
            }
        }

        #endregion

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IObservableEntity, new()
        {
            if (ServiceLocator.IsLocationProviderSet)
            {
                return ServiceLocator.Current.GetInstance<IRepository<TEntity>>();
            }

            return RepositoryAsync<TEntity>();
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, IObservableEntity, new()
        {
            if (ServiceLocator.IsLocationProviderSet)
            {
                return ServiceLocator.Current.GetInstance<IRepositoryAsync<TEntity>>();
            }

            if (_repositories == null)
            {
                _repositories = new Dictionary<string, dynamic>();
            }

            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IRepositoryAsync<TEntity>)_repositories[type];
            }

            var repositoryType = typeof(Repository<>);

            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dbContext, this));

            return _repositories[type];
        }

        #region Unit of Work Transactions

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            _objectContext = ((IObjectContextAdapter)_dbContext).ObjectContext;
            if (_objectContext.Connection.State != ConnectionState.Open)
            {
                _objectContext.Connection.Open();
            }

            _transaction = _objectContext.Connection.BeginTransaction(isolationLevel);
        }

        public bool Commit()
        {
            _transaction.Commit();
            return true;
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _dbContext.SyncObjectsStatePostCommit();
        }

        #endregion
    }
}
