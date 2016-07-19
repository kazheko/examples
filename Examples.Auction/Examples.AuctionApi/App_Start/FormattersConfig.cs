using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Examples.AuctionApi
{
    internal class FormattersConfig
    {
        public static void ConfigFormatters(HttpConfiguration config)
        {
            //config.Formatters.Add(new CollectionJsonFormatter());
            var settings = config.Formatters.JsonFormatter.SerializerSettings;
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/vnd.issue+json"));
        }
    }
}
