using IFeelGoodSalon.Data.Base;
using System;
using System.Data.Entity;

namespace IFeelGoodSalon.Data
{
    public class DefaultDbContextFactory : IDbContextFactory
    {
        public virtual TDbContext CreateDbContext<TDbContext>() where TDbContext : DbContext
        {
            return Activator.CreateInstance<TDbContext>();
        }
    }
}
