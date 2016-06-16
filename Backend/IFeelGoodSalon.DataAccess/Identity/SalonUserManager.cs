using IFeelGoodSalon.Models.Identity;
using Microsoft.AspNet.Identity;
using System;

namespace IFeelGoodSalon.DataAccess.Identity
{
    public class SalonUserManager : UserManager<SalonUser, Guid>
    {
        public SalonUserManager(IUserStore<SalonUser, Guid> store)
            : base(store)
        {
        }
    }
}
