using System.Collections.Generic;
using Examples.AuctionApi.Models;
using Newtonsoft.Json.Linq;

namespace Examples.AuctionApi.MediaTypeFormatters.SirenMediaTypeFormatter.Maps
{
    internal class LotMap
    {
        public JObject Map(Lot lot)
        {
            return new Entity("lot", lot.Id.ToString())

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
    }
}
