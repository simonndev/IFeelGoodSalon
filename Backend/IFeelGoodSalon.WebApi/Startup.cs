using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(IFeelGoodSalon.WebApi.Startup))]

namespace IFeelGoodSalon.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // CORS configuration
            app.UseCors(CorsOptions.AllowAll);

            var config = new HttpConfiguration();

            // OAuth configuration
            ConfigureAuth(app);

            var container = new Container();

            // To allow scoped instances to be resolved during an OWIN request,
            // the following registration needs to be added to the IAppBuilder instance
            app.Use(async (context, next) => {
                using (container.BeginExecutionContextScope())
                {
                    await next();
                }
            });

            // Scoped instances need to be registered with the ExecutionContextScope lifestyle
            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();

            this.RegisterWebApiComponents(container);

            container.RegisterWebApiControllers(config);
            container.Verify();

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            config.EnsureInitialized();

            // Web API configuration
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }

        private void RegisterWebApiComponents(Container container)
        {
            // Register your types, for instance using the scoped lifestyle:
            ////container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);
        }
    }
}