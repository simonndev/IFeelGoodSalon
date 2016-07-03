using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IFeelGoodSalon.Data.BusinessLogic.Base
{
    public interface IBusinessLogicServiceAsync<TEntity> : IBusinessLogicService<TEntity> where TEntity : class
    {
        Task<TEntity> FindAsync(params object[] keyValues);
        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);

        Task<bool> InsertAsync(TEntity entity);
        Task<bool> InsertAsync(CancellationToken cancellationToken, TEntity entity);

        Task<bool> InsertRangeAsync(IEnumerable<TEntity> entities);
        Task<bool> InsertRangeAsync(CancellationToken cancellationToken, IEnumerable<TEntity> entities);

        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> UpdateAsync(CancellationToken cancellationToken, TEntity entity);

        Task<bool> DeleteAsync(params object[] keyValues);
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);
    }
}
