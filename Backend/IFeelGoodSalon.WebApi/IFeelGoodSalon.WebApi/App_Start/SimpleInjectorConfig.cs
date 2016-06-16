using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace IFeelGoodSalon.WebApi
{
    public static class SimpleInjectorConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            ////container.Register<AppDbContext>(Lifestyle.Scoped);
            ////container.Register<IUserStore<AppUser>>(() => new UserStore<AppUser>(container.GetInstance<AppDbContext>()), Lifestyle.Scoped);
            ////container.Register<AppUserManager>(Lifestyle.Scoped);

            // Make the container registrations, example:
            // container.Register<IUserRepository, SqlUserRepository>();

            container.RegisterWebApiControllers(config);

            // Create a new SimpleInjectorDependencyResolver that wraps the,
            // container, and register that resolver in MVC.

            container.Verify();

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}