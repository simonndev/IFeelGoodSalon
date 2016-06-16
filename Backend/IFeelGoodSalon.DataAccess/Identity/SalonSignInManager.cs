using IFeelGoodSalon.Models.Identity;
using System;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Security.Claims;
using IFeelGoodSalon.DataAccess.Extensions;

namespace IFeelGoodSalon.DataAccess.Identity
{
    public class SalonSignInManager : SignInManager<SalonUser, Guid>
    {
        public SalonSignInManager( SalonUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }


        public override Task<ClaimsIdentity> CreateUserIdentityAsync(
            SalonUser user)
        {
            return user.GenerateUserIdentityAsync((SalonUserManager)UserManager);
        }
    }
}
