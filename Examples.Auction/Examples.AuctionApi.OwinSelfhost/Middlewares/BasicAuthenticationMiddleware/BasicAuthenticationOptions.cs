using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security;

namespace Examples.AuctionApi.OwinSelfhost.Middlewares.BasicAuthenticationMiddleware
{
    public class BasicAuthenticationOptions : AuthenticationOptions
    {
        public Func<string, string, Task<AuthenticationTicket>>ValidateCredentials{ get; private set; }
        public string Realm { get; private set; }

        public BasicAuthenticationOptions(string realm, Func<string, string, Task<AuthenticationTicket>> validateCredentials)
            : base("Basic")
        {
            Realm = realm;
            ValidateCredentials = validateCredentials;
        }
    }
}
