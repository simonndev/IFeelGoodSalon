using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace IFeelGoodSalon.WebApi.Filters
{
    /// <summary>
    /// Basic Authentication identity
    /// </summary>
    public class BasicAuthenticationIdentity : GenericIdentity
    {
        public BasicAuthenticationIdentity(string userName, string password)
            : base(userName, "Basic")
        {
            Password = password;
            UserName = userName;
        }

        public string Password { get; set; }
        public string UserName { get; set; }
        public Guid UserId { get; set; } 
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class SalonAuthorizationFilterAttribute : AuthorizationFilterAttribute
    {
        private readonly bool _isActive = true;

        /// <summary>
        /// parameter isActive explicitly enables/disables this filter.
        /// </summary>
        /// <param name="isActive"></param>
        public SalonAuthorizationFilterAttribute(bool isActive)
        {
            _isActive = isActive;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (this._isActive)
            {
                return;
            }

            var identity = FetchAuthHeader(actionContext);
            if (identity == null)
            {
                ChallengeAuthRequest(actionContext);
                return;
            }

            var genericPrincipal = new GenericPrincipal(identity, null);

            Thread.CurrentPrincipal = genericPrincipal;
            if (!OnAuthorizeUser(identity.Name, identity.Password, actionContext))
            {
                ChallengeAuthRequest(actionContext);
                return;
            }

            base.OnAuthorization(actionContext);
        }

        /// <summary>
        /// Checks for autrhorization header in the request and parses it, creates user credentials and returns as BasicAuthenticationIdentity
        /// </summary>
        /// <param name="filterContext"></param>
        protected virtual BasicAuthenticationIdentity FetchAuthHeader(HttpActionContext filterContext)
        {
            string authHeaderValue = string.Empty;

            var authRequest = filterContext.Request.Headers.Authorization;

            if (authRequest != null && !string.IsNullOrEmpty(authRequest.Scheme) && authRequest.Scheme == "Basic")
            {
                authHeaderValue = authRequest.Parameter;
            }

            if (string.IsNullOrEmpty(authHeaderValue))
            {
                return null;
            }

            authHeaderValue = Encoding.Default.GetString(Convert.FromBase64String(authHeaderValue));
            var credentials = authHeaderValue.Split(':');

            return credentials.Length < 2 ? null : new BasicAuthenticationIdentity(credentials[0], credentials[1]);
        }
        
        protected virtual bool OnAuthorizeUser(string username, string password, HttpActionContext filterContext)
        {
            return !(string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password));
        }

        private static void ChallengeAuthRequest(HttpActionContext filterContext)
        {
            var dnsHost = filterContext.Request.RequestUri.DnsSafeHost;
            filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            filterContext.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", dnsHost));
        }
    }
}