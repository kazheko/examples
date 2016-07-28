using System.Web.Http;
using Examples.AuctionApi.OwinSelfhost.Middlewares.BasicAuthenticationMiddleware;
using Owin;

namespace Examples.AuctionApi.OwinSelfhost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            WebApiConfiguration.Configure(config);
            
            app.Use(typeof (BasicAuthenticationMiddleware),
                new BasicAuthenticationOptions("AuctionApi", WebApiConfiguration.ValidateCredentials));
            app.UseWebApi(config);
        }
    }
}
