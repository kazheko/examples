using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security;

namespace Examples.AuctionApi.OwinSelfhost.Middlewares.BasicAuthenticationMiddleware
{
    public class BasicAuthenticationOptions : AuthenticationOptions
    {
        public Func<string, string, Task<bool>>ValidateCredentials{ get; private set; }
        public string Realm { get; private set; }

        public BasicAuthenticationOptions(string realm, Func<string, string, Task<bool>> validateCredentials)
            : base("Basic")
        {
            Realm = realm;
            ValidateCredentials = validateCredentials;
        }
    }
}
