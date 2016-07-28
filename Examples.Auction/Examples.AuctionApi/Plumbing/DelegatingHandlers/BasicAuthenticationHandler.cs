using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Examples.AuctionApi.Plumbing.DelegatingHandlers
{
    public class BasicAuthenticationHandler : DelegatingHandler
    {
        private const string Scheme = "Basic";

        private readonly Func<string, string, Task<bool>> _validateCredentials;

        public BasicAuthenticationHandler(Func<string, string, Task<bool>> validateCredentials)
        {
            _validateCredentials = validateCredentials;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var headers = request.Headers;
            if (headers.Authorization != null && Scheme.Equals(headers.Authorization.Scheme))
            {
                var context = request.GetRequestContext();
                if (context.Principal == null)
                {
                    if (Thread.CurrentPrincipal == null 
                        || !Thread.CurrentPrincipal.Identity.IsAuthenticated)
                    {
                        Thread.CurrentPrincipal = await BuildClaimsPrincipal(headers);
                    }

                    context.Principal = Thread.CurrentPrincipal;
                }
            }
            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(Scheme));
            }

            return response;
        }

        private async Task<ClaimsPrincipal> BuildClaimsPrincipal(HttpRequestHeaders headers)
        {
            string credentials;
            try
            {
                var token = Convert.FromBase64String(headers.Authorization.Parameter);
                credentials = Encoding.UTF8.GetString(token);
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

            if (parts.Length != 2)
                return null;

            var userId = parts[0].Trim();
            var password = parts[1].Trim();

            if (! await _validateCredentials(userId, password))
                return null;

            var claims = new[] {new Claim(ClaimTypes.Name, userId)};
            var claimsIdentities = new[] {new ClaimsIdentity(claims, Scheme)};
            return new ClaimsPrincipal(claimsIdentities);
        }
    }
}
