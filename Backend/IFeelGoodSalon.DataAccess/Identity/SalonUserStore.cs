using IFeelGoodSalon.Models.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace IFeelGoodSalon.DataAccess.Identity
{
    public class SalonUserStore : UserStore<SalonUser, SalonRole, Guid, SalonUserLogin, SalonUserRole, SalonUserClaim>
    {
        public SalonUserStore(IFeelGoodSalonIdentityContext context)
            : base(context)
        {
        }
    }
}
