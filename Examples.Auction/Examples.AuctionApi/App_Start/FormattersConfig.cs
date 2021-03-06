﻿using System.Web.Http;
using Examples.AuctionApi.Plumbing.MediaTypeFormatters.SirenMediaTypeFormatter;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Examples.AuctionApi
{
    internal class FormattersConfig
    {
        public static void RegisterFormatters(HttpConfiguration config)
        {
            config.Formatters.Add(new SirenMediaTypeFormatter());

            var settings = config.Formatters.JsonFormatter.SerializerSettings;
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
