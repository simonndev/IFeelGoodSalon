using IFeelGoodSalon.Models.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFeelGoodSalon.DataAccess.Identity
{
    public class SalonRoleStore : RoleStore<SalonRole, Guid, SalonUserRole>
    {
        public SalonRoleStore(IFeelGoodSalonIdentityContext context)
            : base(context)
        {
        }
    }
}
