using System.Web.Http;

namespace Examples.AuctionApi
{
    internal class RouteConfig
    {
        public const string DefaultApi = "DefaultApi";

        public static void RegisterRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute("Root", "", new { controller = "Home" });
            config.Routes.MapHttpRoute("DefaultApi", "{controller}/{id}", new { id = RouteParameter.Optional });
        }
    }
}
