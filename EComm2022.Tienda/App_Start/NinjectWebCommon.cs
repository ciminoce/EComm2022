using System;
using System.Web;
using EComm2022.Datos;
using EComm2022.Datos.Repositorios;
using EComm2022.Datos.Repositorios.Facades;
using EComm2022.Servicios.Servicios;
using EComm2022.Servicios.Servicios.Facades;
using EComm2022.Tienda;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace EComm2022.Tienda
{
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

            kernel.Bind<IRepositorioCategorias>().To<RepositorioCategorias>().InRequestScope();
            kernel.Bind<IServicioCategorias>().To<ServicioCategorias>().InRequestScope();

            kernel.Bind<IRepositorioMarcas>().To<RepositorioMarcas>().InRequestScope();
            kernel.Bind<IServicioMarcas>().To<ServicioMarcas>().InRequestScope();

            kernel.Bind<IRepositorioProductos>().To<RepositorioProductos>().InRequestScope();
            kernel.Bind<IServicioProductos>().To<ServicioProductos>().InRequestScope();


            kernel.Bind<IRepositorioClientes>().To<RepositorioClientes>().InRequestScope();
            kernel.Bind<IServicioClientes>().To<ServicioClientes>().InRequestScope();

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();

            kernel.Bind<VentasDbContext>().ToSelf().InThreadScope();


        }
    }
}