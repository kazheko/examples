using System;

namespace Examples.AuctionApi.Models
{
    public class Bid
    {
        public Currency Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public virtual User User { get; set; }

    }
}