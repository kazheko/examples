using System;
using System.Threading.Tasks;
using System.Web.Http;
using Examples.AuctionApi.Plumbing.DelegatingHandlers;

namespace Examples.AuctionApi
{
    public static class DelegatingHandlersConfig
    {
        public static void RegisterDelegatingHandlers(HttpConfiguration config, 
            Func<string,string,Task<bool>> validateCredentials)
        {
            config.MessageHandlers.Add(new BasicAuthenticationHandler(validateCredentials));
        }
    }
}
