using IFeelGoodSalon.Data.Base;
using System.Data;

namespace IFeelGoodSalon.Data
{
    public class ReadOnlyDbContextScope : IReadOnlyDbContextScope
    {
        private DbContextScope _internalScope;

        #region Constructors

        public ReadOnlyDbContextScope(IDbContextFactory dbContextFactory = null)
            : this(DbContextScopeOption.JoinExisting, null, dbContextFactory)
        { }

        public ReadOnlyDbContextScope(IsolationLevel isolationLevel, IDbContextFactory dbContextFactory = null)
            : this(DbContextScopeOption.ForceCreateNew, isolationLevel, dbContextFactory)
        { }

        public ReadOnlyDbContextScope(DbContextScopeOption joiningOption, IsolationLevel? isolationLevel, IDbContextFactory dbContextFactory = null)
        {
            _internalScope = new DbContextScope(joiningOption: joiningOption, readOnly: true, isolationLevel: isolationLevel, dbContextFactory: dbContextFactory);
        }

        #endregion

        public IDbContextManagerAsync Manager
        {
            get { return _internalScope.Manager; }
        }

        public void Dispose()
        {
            _internalScope.Dispose();
        }
    }
}
