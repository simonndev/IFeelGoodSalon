using System.Threading;
using System.Threading.Tasks;

namespace IFeelGoodSalon.DataPattern.Ef6.Base
{
    public interface IUnitOfWorkAsync : IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, IObservableEntity, new();
    }
}
