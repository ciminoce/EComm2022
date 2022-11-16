using System.Web.Mvc;
using System.Web.Routing;

namespace EComm2022.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Acceso", action = "LogIn", id = UrlParameter.Optional }
            );
        }
    }
}
