using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace Examples.AuctionApi.MediaTypeFormatters.SirenMediaTypeFormatter
{
    public class SirenMediaTypeFormatter : MediaTypeFormatter
    {
        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return true;
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content,
            TransportContext transportContext)
        {
            return base.WriteToStreamAsync(type, value, writeStream, content, transportContext);
        }
    }
}
