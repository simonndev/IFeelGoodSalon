using System.Data.Entity;

namespace IFeelGoodSalon.Data.Base
{
    public interface IDbContextFactory
    {
        TDbContext CreateDbContext<TDbContext>() where TDbContext : DbContext;
    }
}
