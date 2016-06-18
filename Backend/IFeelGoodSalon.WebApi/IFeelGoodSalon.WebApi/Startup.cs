﻿using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Cors;
using SimpleInjector;
using IFeelGoodSalon.DataPattern.Ef6.Base;
using IFeelGoodSalon.DataAccess;
using IFeelGoodSalon.DataPattern.Ef6;
using SimpleInjector.Extensions.ExecutionContextScoping;
using IFeelGoodSalon.Models;
using SimpleInjector.Integration.WebApi;
using System.Runtime.Remoting.Messaging;
using IFeelGoodSalon.BusinessLogic;

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
        /// http://simpleinjector.readthedocs.io/en/latest/lifetimes.html
        /// </remarks>
        public void Configuration(IAppBuilder app)
        {

            HttpConfiguration config = new HttpConfiguration();

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);

            var container = new Container();

            // To allow scoped instances to be resolved during an OWIN request,
            // the following registration needs to be added to the IAppBuilder instance:
            app.Use(async (context, next) =>
            {
                using (container.BeginExecutionContextScope())
                {
                    await next();
                }
            });

            /**
             * There will be only one instance of a given service type within a certain (explicitly defined) scope and
             * that instance will be disposed when the scope ends(unless specified otherwise).
             * This scope will automatically flow with the logical flow of control of asynchronous methods.
             **/
            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();

            RegisterComponents(container);

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            WebApiConfig.Register(config);
        }

        /// <summary>
        /// Registers components within an explicitly defined scope.
        /// 
        /// <code>
        /// using (container.BeginExecutionContextScope()) {
        ///     var uow1 = container.GetInstance<IUnitOfWork>();
        ///     await SomeAsyncOperation();
        ///     
        ///     var uow2 = container.GetInstance<IUnitOfWork>();
        ///     await SomeOtherAsyncOperation();
        ///     
        ///     Assert.AreSame(uow1, uow2);
        /// }
        /// </code>
        /// 
        /// Scopes can be nested and each scope will get its own set of instances:
        /// 
        /// <code>
        /// using (container.BeginExecutionContextScope()) {
        ///     var outer1 = container.GetInstance<IUnitOfWork>();
        ///     await SomeAsyncOperation();
        /// 
        ///     var outer2 = container.GetInstance<IUnitOfWork>();
        ///     Assert.AreSame(outer1, outer2);
        /// 
        ///     using (container.BeginExecutionContextScope()) {
        ///         var inner1 = container.GetInstance<IUnitOfWork>();
        ///         await SomeOtherAsyncOperation();
        /// 
        ///         var inner2 = container.GetInstance<IUnitOfWork>();
        ///         
        ///         Assert.AreSame(inner1, inner2);
        ///         Assert.AreNotSame(outer1, inner1);
        ///     }
        /// }
        /// </code>
        /// </summary>
        /// <param name="container"></param>
        private static void RegisterComponents(Container container)
        {
            container.Register<IObservableDbContextAsync, IFeelGoodSalonContext>(Lifestyle.Scoped);
            container.Register<IUnitOfWorkAsync, UnitOfWork>(Lifestyle.Scoped);

            container.Register<IRepositoryAsync<Treatment>, Repository<Treatment>>(Lifestyle.Scoped);

            container.Register<IUserService, UserService>(Lifestyle.Scoped);

            container.Verify();
        }
    }
}
 