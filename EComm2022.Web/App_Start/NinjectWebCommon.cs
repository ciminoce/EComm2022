[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(EComm2022.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(EComm2022.Web.App_Start.NinjectWebCommon), "Stop")]

namespace EComm2022.Web.App_Start
{
    using System;
    using System.Web;
    using EComm2022.Datos;
    using EComm2022.Datos.Repositorios;
    using EComm2022.Datos.Repositorios.Facades;
    using EComm2022.Servicios.Servicios;
    using EComm2022.Servicios.Servicios.Facades;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
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
            kernel.Bind<IRepositorioProveedores>().To<RepositorioProveedores>().InRequestScope();
            kernel.Bind<IServicioProveedores>().To<ServicioProveedores>().InRequestScope();


            kernel.Bind<IRepositorioCiudades>().To<RepositorioCiudades>().InRequestScope();
            kernel.Bind<IServicioCiudades>().To<ServicioCiudades>().InRequestScope();


            kernel.Bind<IRepositorioProvincias>().To<RepositorioProvincias>().InRequestScope();
            kernel.Bind<IServicioProvincias>().To<ServicioProvincias>().InRequestScope();

            kernel.Bind<IRepositorioCategorias>().To<RepositorioCategorias>().InRequestScope();
            kernel.Bind<IServicioCategorias>().To<ServicioCategorias>().InRequestScope();

            kernel.Bind<IRepositorioMarcas>().To<RepositorioMarcas>().InRequestScope();
            kernel.Bind<IServicioMarcas>().To<ServicioMarcas>().InRequestScope();

            kernel.Bind<IRepositorioProductos>().To<RepositorioProductos>().InRequestScope();
            kernel.Bind<IServicioProductos>().To<ServicioProductos>().InRequestScope();

            kernel.Bind<IRepositorioUsuarios>().To<RepositorioUsuarios>().InRequestScope();
            kernel.Bind<IServicioUsuarios>().To<ServicioUsuarios>().InRequestScope();

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();

            kernel.Bind<VentasDbContext>().ToSelf().InSingletonScope();

        }
    }
}