using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Cors;

[assembly: OwinStartup(typeof(IFeelGoodSalon.WebApi.Startup))]

namespace IFeelGoodSalon.WebApi
{
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <remarks>
        /// Regarding app.CreatePerOwinContext - I would be very careful in using this line:
        /// 
        /// <code>
        /// app.CreatePerOwinContext(() => container.GetInstance<AppUserManager>());
        /// </code>
        /// 
        /// You are requesting an instance of a LifeStyle.Scoped type inside Owin,
        /// but you declared the default scope as WebApiRequestLifestyle.
        /// This life-style inherits from ExecutionContextScopeLifestyle but
        /// the scope itself begins only on the start of a Web Api request,
        /// and thus I believe it will not be present in any middleware executed outside of Web API
        /// (maybe Steven could help us clarify this matter).
        /// 
        /// You may consider using WebRequestLifestyle, but be aware of the possible issues with async/await.
        /// </remarks>
        public void Configuration(IAppBuilder app)
        {

            HttpConfiguration config = new HttpConfiguration();

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);

            WebApiConfig.Register(config);
            SimpleInjectorConfig.Register(config);

            
        }
    }
}