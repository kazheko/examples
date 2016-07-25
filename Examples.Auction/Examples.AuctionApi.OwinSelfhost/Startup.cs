using System.Web.Http;
using Examples.AuctionApi.OwinSelfhost.Middlewares.BasicAuthenticationMiddleware;
using Owin;

namespace Examples.AuctionApi.OwinSelfhost
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            WebApiConfiguration.Configure(config);
            appBuilder.UseWebApi(config);
            appBuilder.Use(typeof (BasicAuthnMiddleware), new BasicAuthenticationOptions("AuctionApi", null));
        }
    }
}
