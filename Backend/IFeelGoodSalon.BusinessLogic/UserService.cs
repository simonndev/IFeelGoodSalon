using IFeelGoodSalon.BusinessLogic.Base;
using IFeelGoodSalon.Common.Security;
using IFeelGoodSalon.Data.Base;
using IFeelGoodSalon.Data.BusinessLogic.Base;
using IFeelGoodSalon.Data.Repository.Base;
using IFeelGoodSalon.Models;
using System;
using System.Linq;

namespace IFeelGoodSalon.BusinessLogic
{
    public interface IUserService : IBusinessLogicServiceAsync<User>
    {
        bool Authenticate(string username, string password, out Guid userId);
    }

    public class UserService : BusinessLogicService<User>, IUserService
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepositoryAsync<User> _repository;

        public UserService(IDbContextScopeFactory dbContextScopeFactory, IRepositoryAsync<User> repository)
            : base(dbContextScopeFactory, repository)
        {
            this._dbContextScopeFactory = dbContextScopeFactory;
            this._repository = repository;
        }

        public bool Authenticate(string username, string password, out Guid userId)
        {
            userId = Guid.Empty;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            // 1. Retrieve user using the specific username.
            var user = this.Queryable().SingleOrDefault(u => u.Username == username);
            if (user != null)
            {
                // 2. Compare password using BCrypt.Net
                bool isMatch = BCrypt.Verify(password, user.PasswordHashed);
                if (isMatch)
                {
                    userId = user.Id;
                    return true;
                }
            }

            return false;
        }
    }
}
