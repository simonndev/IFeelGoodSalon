using IFeelGoodSalon.Data.Base;
using System.Data.Entity;

namespace IFeelGoodSalon.Data
{
    public class AmbientDbContextLocator : IAmbientDbContextLocator
    {
        public TDbContext Get<TDbContext>() where TDbContext : DbContext
        {
            var ambientDbContextScope = DbContextScope.GetAmbientScope();

            return ambientDbContextScope == null ? null : ambientDbContextScope.Manager.Get<TDbContext>();
        }
    }
}
