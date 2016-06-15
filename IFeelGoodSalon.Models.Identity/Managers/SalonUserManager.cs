using Microsoft.AspNet.Identity;
using System;

namespace IFeelGoodSalon.Models.Identity.Managers
{
    public class SalonUserManager : UserManager<SalonUser, Guid>
    {
        public SalonUserManager(IUserStore<SalonUser, Guid> userStore)
            : base(userStore)
        {
        }
    }
}