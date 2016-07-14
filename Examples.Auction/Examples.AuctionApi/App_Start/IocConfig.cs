using System.Web.Http;
using System.Web.Http.Dispatcher;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Examples.AuctionApi.Plumbing;

namespace Examples.AuctionApi
{
    internal class IocConfig
    {
        private IWindsorContainer _container;

        public void ConfigContainer(HttpConfiguration config)
        {
            _container = new WindsorContainer().Install(FromAssembly.This());
            var service = new WindsorCompositionRoot(_container);
            config.Services.Replace(typeof(IHttpControllerActivator), service);
        }
    }
}
