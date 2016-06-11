using System.Threading;
using System.Threading.Tasks;

namespace IFeelGoodSalon.DataPattern.Ef6.Base
{
    public interface IObservableDbContextAsync : IObservableDbContext
    {
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
