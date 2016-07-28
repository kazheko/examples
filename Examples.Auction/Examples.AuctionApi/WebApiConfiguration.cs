using System.Threading.Tasks;
using System.Web.Http;
using Examples.AuctionApi.Infrastructure;

namespace Examples.AuctionApi
{
    public class WebApiConfiguration
    {
        public static void Configure(HttpConfiguration config)
        {
            IocConfig.ConfigContainer(config);
            RouteConfig.RegisterRoutes(config);
            FormattersConfig.RegisterFormatters(config);

            DelegatingHandlersConfig.RegisterDelegatingHandlers(config, ValidateCredentials);
        }

        public static async Task<bool> ValidateCredentials(string userId, string password)
        {
            return await new InMemoryUserStore().Validate(userId, password);
        }
    }
}
