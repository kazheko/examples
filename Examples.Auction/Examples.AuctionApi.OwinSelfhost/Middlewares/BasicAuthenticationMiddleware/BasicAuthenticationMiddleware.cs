using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;

namespace Examples.AuctionApi.OwinSelfhost.Middlewares.BasicAuthenticationMiddleware
{
    public class BasicAuthenticationMiddleware : AuthenticationMiddleware<BasicAuthenticationOptions>
    {
        public BasicAuthenticationMiddleware(
            OwinMiddleware next,
            BasicAuthenticationOptions options)
            : base(next, options)
        { }

        protected override AuthenticationHandler<BasicAuthenticationOptions> CreateHandler()
        {
            return new BasicAuthenticationHandler(Options);
        }
    }

    internal class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        private readonly string _challenge;

        public BasicAuthenticationHandler(BasicAuthenticationOptions options)
        {
            _challenge = "Basic realm=" + options.Realm;
        }

        protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            var authorization = Request.Headers.Get("Authorization");
            if (string.IsNullOrEmpty(authorization) || !authorization.StartsWith("Basic ",StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var token = authorization.Substring("Basic ".Length).Trim();

            var identity = await BuildClaimsIdentity(token, Options.ValidateCredentials);
            return new AuthenticationTicket(identity, new AuthenticationProperties());
        }

        protected override Task ApplyResponseChallengeAsync()
        {
            if (Response.StatusCode == 401)
            {
                var challenge = Helper.LookupChallenge(Options.AuthenticationType, Options.AuthenticationMode);
                if (challenge != null)
                {
                    Response.Headers.AppendValues("WWW-Authenticate", _challenge);
                }
            }
            return Task.FromResult<object>(null);
        }

        public static async Task<ClaimsIdentity> BuildClaimsIdentity(string credentials, Func<string, string, Task<bool>> validate)
        {
            try
            {
                credentials = Encoding.UTF8.GetString(Convert.FromBase64String(credentials));
            }
            catch (FormatException)
            {
                return null;
            }
            catch (ArgumentException)
            {
                return null;
            }

            var parts = credentials.Split(':');
            if (parts.Length != 2) return null;

            var userId = parts[0].Trim();
            var password = parts[1].Trim();

            var isValid = await validate(userId, password);
            if (!isValid) return null;

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userId),
                new Claim(ClaimTypes.Country, "Речь Паспалитая"), 
            };

            return new ClaimsIdentity(claims);
        }
    }
}
