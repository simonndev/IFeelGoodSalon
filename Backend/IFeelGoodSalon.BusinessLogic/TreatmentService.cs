using IFeelGoodSalon.BusinessLogic.Base;
using IFeelGoodSalon.Data.Base;
using IFeelGoodSalon.Data.BusinessLogic.Base;
using IFeelGoodSalon.Data.Repository.Base;
using IFeelGoodSalon.Models;

namespace IFeelGoodSalon.BusinessLogic
{
    public interface ITreatmentService : IBusinessLogicServiceAsync<Treatment>
    {
    }
    public class TreatmentService : BusinessLogicService<Treatment>, ITreatmentService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepositoryAsync<Treatment> _repository;

        public TreatmentService(IDbContextScopeFactory dbContextScopeFactory, IRepositoryAsync<Treatment> repository)
            : base(dbContextScopeFactory, repository)
        {
            this._dbContextScopeFactory = dbContextScopeFactory;
            this._repository = repository;
        }
    }
}
