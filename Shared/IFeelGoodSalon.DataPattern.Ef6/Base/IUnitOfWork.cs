using System;
using System.Data;

namespace IFeelGoodSalon.DataPattern.Ef6.Base
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);

        bool Commit();
        void Rollback();

        int SaveChanges();

        IRepository<TEntity> Repository<TEntity>() where TEntity : class, IObservableEntity, new();
    }
}
