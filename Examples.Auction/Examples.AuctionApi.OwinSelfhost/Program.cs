using Microsoft.Owin.Hosting;
using System;
using Examples.AuctionApi.OwinSelfhost;

namespace OwinSelfhostSample
{
    public class Program
    {
        static void Main()
        {
            var baseAddress = "http://localhost:7000/";

            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine($"AuctionApi hosted at: {baseAddress}");
                Console.ReadLine();
            }
            
        }
    }
}