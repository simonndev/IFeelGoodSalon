using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IFeelGoodSalon.Data.Base
{
    public interface IDbContextManagerAsync : IDbContextManager
    {
        Task<int> CommitAsync();
        Task<int> CommitAsync(CancellationToken token);
    }
}
