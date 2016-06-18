using IFeelGoodSalon.Common.Security;
using IFeelGoodSalon.DataPattern.Ef6;
using IFeelGoodSalon.DataPattern.Ef6.Base;
using IFeelGoodSalon.Models;
using System;
using System.Linq;

namespace IFeelGoodSalon.BusinessLogic
{
    public interface IUserService : IBusinessService<User>
    {
        bool Authenticate(string username, string password, out Guid userId);
    }

    public class UserService : BusinessService<User>, IUserService
    {
        private readonly IRepositoryAsync<User> _repository;

        public UserService(IRepositoryAsync<User> repository)
            : base(repository)
        {
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
            var user = this._repository.Queryable().SingleOrDefault(u => u.Username == username);
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
