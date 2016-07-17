﻿using System;
using System.Collections.Generic;

namespace Examples.AuctionApi.Models
{
    public class Lot
    {
        public Lot()
        {
            Bids = new List<Bid>();        
        }

        public Lot(ICollection<Bid> bids)
        {
            Bids = bids;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Currency StartPrice { get; set; }
        public Currency CurrentPrice { get; set; }
        public User Owner { get; set; }
        public ICollection<Bid> Bids { get; private set; }
    }
}
