using System.Threading;
using System.Threading.Tasks;

namespace IFeelGoodSalon.DataPattern.Ef6.Base
{
    public interface IRepositoryAsync<TEntity> : IRepository<TEntity> where TEntity : class, IObservableEntity, new()
    {
        Task<bool> DeleteAsync(params object[] keyValues);
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);

        Task<TEntity> FindAsync(params object[] keyValues);
        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
    }
}
