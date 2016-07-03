using System.Collections.Generic;
using System.Threading.Tasks;

namespace IFeelGoodSalon.Data.Query.Base
{
    public interface IQueryFluentAsync<TEntity> : IQueryFluent<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> SelectAsync();
    }
}
