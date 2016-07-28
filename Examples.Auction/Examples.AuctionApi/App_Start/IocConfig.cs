using System.Web.Http;
using System.Web.Http.Dispatcher;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Examples.AuctionApi.Plumbing.HttpControllerActivators;

namespace Examples.AuctionApi
{
    internal class IocConfig
    {
        public static void ConfigContainer(HttpConfiguration config)
        {
            var container = new WindsorContainer().Install(FromAssembly.This());
            var service = new WindsorCompositionRoot(container);
            config.Services.Replace(typeof(IHttpControllerActivator), service);
        }
    }
}
