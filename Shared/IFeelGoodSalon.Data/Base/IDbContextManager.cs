using System;
using System.Data.Entity;

namespace IFeelGoodSalon.Data.Base
{
    /// <summary>
    /// Maintains a list of lazily-created DbContext instances.
    /// </summary>
    public interface IDbContextManager : IDisposable
    {
        int Commit();
        void Rollback();

        /// <summary>
        /// Gets/Creates a DbContext instance of the specified type. 
        /// </summary>
        TDbContext Get<TDbContext>() where TDbContext : DbContext;
    }
}
