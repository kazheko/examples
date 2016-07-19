using System.Web.Http;

namespace Examples.AuctionApi
{
    public static class WebApiConfiguration
    {
        public static void Configure(HttpConfiguration config)
        {
            RouteConfig.RegisterRoutes(config);
            IocConfig.ConfigContainer(config);
            FormattersConfig.ConfigFormatters(config);
        }
    }
}
