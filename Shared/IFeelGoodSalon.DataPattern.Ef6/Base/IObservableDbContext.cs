using System;

namespace IFeelGoodSalon.DataPattern.Ef6.Base
{
    public interface IObservableDbContext : IDisposable
    {
        int SaveChanges();

        void SyncObjectsStatePostCommit();

        void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObservableEntity;
    }
}
