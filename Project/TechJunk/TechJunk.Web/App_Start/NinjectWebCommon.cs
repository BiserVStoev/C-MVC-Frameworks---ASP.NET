[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TechJunk.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(TechJunk.Web.App_Start.NinjectWebCommon), "Stop")]

namespace TechJunk.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using TechJunk.Services;
    using TechJunk.Services.Interfaces;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IHomeService>().To<HomeService>();
            kernel.Bind<IInterestsService>().To<InterestsService>();
            kernel.Bind<ISalesService>().To<SalesService>();
            kernel.Bind<IUsersService>().To<UsersService>();
            kernel.Bind<IAdminFeedbackService>().To<AdminFeedbackService>();
            kernel.Bind<IAdminInterestsService>().To<AdminInterestsService>();
            kernel.Bind<IAdminUsersService>().To<AdminUsersService>();
            kernel.Bind<IAdminSalesService>().To<AdminSalesService>();
            kernel.Bind<IMessagesService>().To<MessagesService>();
        }        
    }
}
