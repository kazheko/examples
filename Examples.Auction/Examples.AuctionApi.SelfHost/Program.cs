using System;
using System.Web.Http.SelfHost;

namespace Examples.AuctionApi.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new HttpSelfHostConfiguration("http://localhost:7000");
            WebApiConfiguration.Configure(config);
            var host = new HttpSelfHostServer(config);
            host.OpenAsync().Wait();
            Console.WriteLine($"AuctionApi hosted at: {config.BaseAddress}");
            Console.ReadLine();
        }
    }
}
