using IFeelGoodSalon.Data.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace IFeelGoodSalon.Data
{
    public class DbContextManager : IDbContextManagerAsync
    {
        private readonly IDbContextFactory _dbContextFactory;

        private readonly Dictionary<Type, DbContext> _initializedDbContexts =
            new Dictionary<Type, DbContext>();

        private readonly Dictionary<DbContext, DbContextTransaction> _transactions =
            new Dictionary<DbContext, DbContextTransaction>();

        private IsolationLevel? _isolationLevel;

        private bool _isDisposed;
        private bool _isCompleted;
        private bool _isReadOnly;

        public DbContextManager(bool readOnly = false, IsolationLevel? isolationLevel = null, IDbContextFactory dbContextFactory = null)
        {
            this._isReadOnly = readOnly;
            this._isolationLevel = isolationLevel;
            this._dbContextFactory = dbContextFactory;
        }

        internal Dictionary<Type, DbContext> InitializedDbContexts
        {
            get { return this._initializedDbContexts; }
        }

        public int Commit()
        {
            if (this._isDisposed)
            {
                throw new ObjectDisposedException("DbContextManager");
            }

            if (this._isCompleted)
            {
                throw new InvalidOperationException("You can't call Commit() or Rollback() more than once on a DbContextCollection. All the changes in the DbContext instances managed by this collection have already been saved or rollback and all database transactions have been completed and closed. If you wish to make more data changes, create a new DbContextCollection and make your changes there.");
            }

            // Best effort. You'll note that we're not actually implementing an atomic commit
            // here. It entirely possible that one DbContext instance will be committed successfully
            // and another will fail. Implementing an atomic commit would require us to wrap
            // all of this in a TransactionScope. The problem with TransactionScope is that
            // the database transaction it creates may be automatically promoted to a
            // distributed transaction if our DbContext instances happen to be using different
            // databases. And that would require the DTC service (Distributed Transaction Coordinator)
            // to be enabled on all of our live and dev servers as well as on all of our dev workstations.
            // Otherwise the whole thing would blow up at runtime.

            // In practice, if our services are implemented following a reasonably DDD approach,
            // a business transaction (i.e. a service method) should only modify entities in a single
            // DbContext. So we should never find ourselves in a situation where two DbContext instances
            // contain uncommitted changes here. We should therefore never be in a situation where the below
            // would result in a partial commit.

            ExceptionDispatchInfo lastError = null;

            var effort = 0;

            foreach (var dbContext in this._initializedDbContexts.Values)
            {
                try
                {
                    if (!this._isReadOnly)
                    {
                        effort += dbContext.SaveChanges();
                    }

                    // If we've started an explicit database transaction, time to commit it now.
                    DbContextTransaction transaction = null;
                    bool succeed = this._transactions.TryGetValue(dbContext, out transaction);
                    if (succeed)
                    {
                        transaction.Commit();
                        transaction.Dispose();
                    }
                }
                catch (Exception e)
                {
                    lastError = ExceptionDispatchInfo.Capture(e);
                }
            }

            this._transactions.Clear();
            this._isCompleted = true;

            if (lastError != null)
            {
                lastError.Throw(); // Re-throw while maintaining the exception's original stack track
            }

            return effort;
        }

        public Task<int> CommitAsync()
        {
            return CommitAsync(CancellationToken.None);
        }

        public async Task<int> CommitAsync(CancellationToken cancelToken)
        {
            if (cancelToken == null)
            {
                throw new ArgumentNullException("cancelToken");
            }

            if (this._isDisposed)
            {
                throw new ObjectDisposedException("DbContextManager");
            }

            if (this._isCompleted)
            {
                throw new InvalidOperationException("You can't call Commit() or Rollback() more than once on a DbContextCollection. All the changes in the DbContext instances managed by this collection have already been saved or rollback and all database transactions have been completed and closed. If you wish to make more data changes, create a new DbContextCollection and make your changes there.");
            }

            // See comments in the sync version of this method for more details.

            ExceptionDispatchInfo lastError = null;

            var effort = 0;

            foreach (var dbContext in _initializedDbContexts.Values)
            {
                try
                {
                    if (!this._isReadOnly)
                    {
                        effort += await dbContext.SaveChangesAsync(cancelToken).ConfigureAwait(false);
                    }

                    // If we've started an explicit database transaction, time to commit it now.
                    DbContextTransaction transaction = null;
                    bool succeed = this._transactions.TryGetValue(dbContext, out transaction);
                    if (succeed)
                    {
                        transaction.Commit();
                        transaction.Dispose();
                    }
                }
                catch (Exception e)
                {
                    lastError = ExceptionDispatchInfo.Capture(e);
                }
            }

            this._transactions.Clear();
            this._isCompleted = true;

            if (lastError != null)
            {
                lastError.Throw(); // Re-throw while maintaining the exception's original stack track
            }

            return effort;
        }

        public void Rollback()
        {
            if (this._isDisposed)
            {
                throw new ObjectDisposedException("DbContextCollection");
            }

            if (this._isCompleted)
            {
                throw new InvalidOperationException("You can't call Commit() or Rollback() more than once on a DbContextCollection. All the changes in the DbContext instances managed by this collection have already been saved or rollback and all database transactions have been completed and closed. If you wish to make more data changes, create a new DbContextCollection and make your changes there.");
            }

            ExceptionDispatchInfo lastError = null;

            foreach (var dbContext in _initializedDbContexts.Values)
            {
                // There's no need to explicitly rollback changes in a DbContext as
                // DbContext doesn't save any changes until its SaveChanges() method is called.
                // So "rolling back" for a DbContext simply means not calling its SaveChanges()
                // method.

                // But if we've started an explicit database transaction, then we must roll it back.
                DbContextTransaction transaction = null;
                bool succeed = this._transactions.TryGetValue(dbContext, out transaction);
                if (succeed)
                {
                    try
                    {
                        transaction.Rollback();
                        transaction.Dispose();
                    }
                    catch (Exception e)
                    {
                        lastError = ExceptionDispatchInfo.Capture(e);
                    }
                }
            }

            this._transactions.Clear();
            this._isCompleted = true;

            if (lastError != null)
            {
                lastError.Throw(); // Re-throw while maintaining the exception's original stack track
            }
        }

        public void Dispose()
        {
            if (this._isDisposed)
                return;

            // Do our best here to dispose as much as we can even if we get errors along the way.
            // Now is not the time to throw. Correctly implemented applications will have called
            // either Commit() or Rollback() first and would have got the error there.

            if (!this._isCompleted)
            {
                try
                {
                    if (this._isReadOnly)
                    {
                        Commit();
                    }
                    else
                    {
                        Rollback();
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }

            foreach (var dbContext in _initializedDbContexts.Values)
            {
                try
                {
                    dbContext.Dispose();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }

            _initializedDbContexts.Clear();
            _isDisposed = true;
        }

        public TDbContext Get<TDbContext>() where TDbContext : DbContext
        {
            if (this._isDisposed)
            {
                throw new ObjectDisposedException("DbContextManager");
            }

            var requestedType = typeof(TDbContext);

            if (!this._initializedDbContexts.ContainsKey(requestedType))
            {
                // First time we've been asked for this particular DbContext type.
                // Create one, cache it and start its database transaction if needed.
                var dbContext = this._dbContextFactory != null
                    ? this._dbContextFactory.CreateDbContext<TDbContext>()
                    : Activator.CreateInstance<TDbContext>();

                this._initializedDbContexts.Add(requestedType, dbContext);

                if (this._isReadOnly)
                {
                    dbContext.Configuration.AutoDetectChangesEnabled = false;
                }

                if (this._isolationLevel.HasValue)
                {
                    var transaction = dbContext.Database.BeginTransaction(this._isolationLevel.Value);
                    this._transactions.Add(dbContext, transaction);
                }
            }

            return this._initializedDbContexts[requestedType] as TDbContext;
        }

        /// <summary>
        /// Returns the value associated with the specified key or the default
        /// value for the TValue  type.
        /// </summary>
        private static TValue GetValueOrDefault<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : default(TValue);
        }
    }
}