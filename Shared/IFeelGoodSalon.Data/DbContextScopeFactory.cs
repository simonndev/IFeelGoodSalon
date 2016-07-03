using IFeelGoodSalon.Data.Base;
using System;
using System.Data;

namespace IFeelGoodSalon.Data
{
    public class DbContextScopeFactory : IDbContextScopeFactory
    {
        private readonly IDbContextFactory _dbContextFactory;

        public DbContextScopeFactory(IDbContextFactory dbContextFactory = null)
        {
            _dbContextFactory = dbContextFactory;
        }

        public IDbContextScopeAsync Create(DbContextScopeOption joiningOption = DbContextScopeOption.JoinExisting)
        {
            return new DbContextScope(joiningOption, false, null, _dbContextFactory);
        }

        public IReadOnlyDbContextScope CreateReadOnly(DbContextScopeOption joiningOption = DbContextScopeOption.JoinExisting)
        {
            return new ReadOnlyDbContextScope(joiningOption, null, _dbContextFactory);
        }

        public IDbContextScopeAsync CreateWithTransaction(IsolationLevel isolationLevel)
        {
            return new DbContextScope(DbContextScopeOption.ForceCreateNew, false, isolationLevel, _dbContextFactory);
        }

        public IReadOnlyDbContextScope CreateReadOnlyWithTransaction(IsolationLevel isolationLevel)
        {
            return new ReadOnlyDbContextScope(DbContextScopeOption.ForceCreateNew, isolationLevel, _dbContextFactory);
        }

        public IDisposable SuppressAmbientContext()
        {
            return new AmbientDbContextSuppressor();
        }
    }
}
