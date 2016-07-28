using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Examples.AuctionApi.Models;
using Newtonsoft.Json.Linq;

namespace Examples.AuctionApi.Plumbing.MediaTypeFormatters.SirenMediaTypeFormatter
{
    public class SirenMediaTypeFormatter : JsonMediaTypeFormatter
    {
        public SirenMediaTypeFormatter()
        {
            var header = new MediaTypeHeaderValue("application/vnd.siren+json");
            SupportedMediaTypes.Add(header);

            var mapping = new QueryStringMapping("format", "siren", header);
            MediaTypeMappings.Add(mapping);
        }

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
            dynamic obj = Convert.ChangeType(value, type);
            value = BuildEntity(obj);
            return base.WriteToStreamAsync(value.GetType(), value, writeStream, content, transportContext);
        }

        private static JObject BuildEntity(Lot lot)
        {
            if (lot == null) throw new ArgumentNullException();

            return new EntityBuilder("lot", lot.Id.ToString())

                .Properties("Id", lot.Id.ToString())
                .Properties("Title", lot.Title)
                .Properties("Description", lot.Description)
                .Properties("CurrentPrice", lot.CurrentPrice.ToString())
                .Properties("StartTime", lot.StartTime.ToShortDateString())
                .Properties("EndTime", lot.EndTime.ToShortDateString())
                .Properties("StartPrice", lot.StartPrice.ToString())

                .EmbeddedLink("bids", null)

                .EmbeddedRepresentation("owner", lot.Owner.Username,
                    new Dictionary<string, string> {{"username", lot.Owner.Username}})

                .Action("bids", "add-bid", "POST",
                    new Dictionary<string, string> {{"lotId", lot.Id.ToString()}, {"userId", null}, {"amount", null}})

                .Build();
        }

        private static JObject BuildEntity(IEnumerable<Lot> lots)
        {
            if (lots == null) throw new ArgumentNullException();

            var builder = new EntityBuilder("lot")
                .Properties("size", lots.Count().ToString());

            foreach (var lot in lots)
            {
                builder.EmbeddedRepresentation("lot", lot.Id.ToString(), new Dictionary<string, string>
                {
                    {"Id",lot.Id.ToString()},
                    {"Title",lot.Title},
                    {"Title",lot.StartTime.ToLongDateString()},
                    {"EndTime",lot.EndTime.ToLongDateString()},
                    {"Title",lot.CurrentPrice.ToString()},
                });
            }
            return builder.Build();
        }

        private static JObject BuildEntity(IEnumerable<Bid> bids)
        {
            if (bids == null) throw new ArgumentNullException();

            var builder = new EntityBuilder("lot")
                .Properties("size", bids.Count().ToString());

            foreach (var bid in bids)
            {
                builder.EmbeddedRepresentation("lot", null, new Dictionary<string, string>
                {
                    {"Id",bid.Amount.ToString()},
                    {"Title",bid.Timestamp.ToLongDateString()},
                    {"Title",bid.User.Username}
                });
            }
            return builder.Build();
        }
    }
}
