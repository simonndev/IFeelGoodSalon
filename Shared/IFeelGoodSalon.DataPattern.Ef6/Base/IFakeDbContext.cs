using IFeelGoodSalon.DataPattern.Ef6.Fake;
using System.Data.Entity;

namespace IFeelGoodSalon.DataPattern.Ef6.Base
{
    public interface IFakeDbContext : IObservableDbContextAsync
    {
        void AddFakeDbSet<TEntity, TFakeDbSet>() where TEntity : ObservableEntity, new() where TFakeDbSet : FakeDbSet<TEntity>, IDbSet<TEntity>, new();

        DbSet<T> Set<T>() where T : class;
    }
}
