using IFeelGoodSalon.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;

namespace IFeelGoodSalon.WebApi.Filters
{
    public class ApiAuthenticationFilter : SalonAuthorizationFilterAttribute
    {
        public ApiAuthenticationFilter(bool isActive)
            : base(isActive)
        {
        }

        protected override bool OnAuthorizeUser(string username, string password, HttpActionContext actionContext)
        {
            var provider = actionContext.ControllerContext.Configuration
                           .DependencyResolver.GetService(typeof(IUserService)) as IUserService;

            if (provider != null)
            {
                Guid userId = Guid.Empty;

                bool result = provider.Authenticate(username, password, out userId);
                if (result)
                {
                    var basicAuthenticationIdentity = Thread.CurrentPrincipal.Identity as BasicAuthenticationIdentity;
                    if (basicAuthenticationIdentity != null)
                    {
                        basicAuthenticationIdentity.UserId = userId;
                    }

                    return true;
                }
            }

            return false;
        }
    }
}