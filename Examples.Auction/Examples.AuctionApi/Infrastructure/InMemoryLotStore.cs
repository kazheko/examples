using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examples.AuctionApi.Models;

namespace Examples.AuctionApi.Infrastructure
{
    public class InMemoryLotStore : ILotStore
    {
        private readonly IList<Lot> _lots;
        private int _id;

        public InMemoryLotStore(IUserStore userStore)
        {
            _lots = new List<Lot>();

            GenerateData(userStore);
        }

        public Task<IEnumerable<Lot>> FindAsync()
        {
            return Task.FromResult(_lots.AsEnumerable());
        }

        public Task<Lot> FindAsync(int lotId)
        {
            return Task.FromResult(_lots.SingleOrDefault(i => i.Id == lotId));
        }

        public Task<IEnumerable<Lot>> FindAsyncQuery(string searchText)
        {
            return Task.FromResult(_lots.Where(
                i => i.Title.Contains(searchText) || i.Description.Contains(searchText)));
        }

        public Task UpdateAsync(Lot lot)
        {
            var oldLot = FindAsync(lot.Id).Result;
            oldLot.Title = lot.Title;
            oldLot.Description = lot.Description;
            return Task.FromResult("");
        }

        public Task DeleteAsync(int lotId)
        {
            var lot = _lots.Single(i => i.Id == lotId);
            _lots.Remove(lot);
            return Task.FromResult("");
        }

        public Task CreateAsync(Lot lot)
        {
            lot.Id = ++_id;
            _lots.Add(lot);
            return Task.FromResult(lot);
        }

        public Task AddBid(int lotId, Bid bid)
        {
            var lot = FindAsync(lotId).Result;

            if (lot == null) return null;

            if (!lot.CurrentPrice.Code.Equals(bid.Amount.Code)) return null;

            if (lot.Bids.Any(x => x.Amount.Value >= bid.Amount.Value)) return null;
            
            lot.Bids.Add(bid);

            return Task.FromResult(bid);
        }

        private void GenerateData(IUserStore userStore)
        {
            CreateAsync(new Lot
            {
                CurrentPrice = new Currency("$125.66"),
                Title = "HTC One M8 16GB Gunmetal Grey Unlocked",
                Description = "An item that has been used previously",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMonths(1),
                StartPrice = new Currency("$125.66"),
                Owner = userStore.CreateAsync(new User { Email = "jack@gmail.com", Username = "jack" }).Result
            });

            CreateAsync(new Lot
            {
                CurrentPrice = new Currency("$299.99"),
                Title = "HTC One M9 32GB",
                Description = "A brand-new, unused, unopened, undamaged item in its original packaging",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMonths(1),
                StartPrice = new Currency("$320.00"),
                Owner = userStore.CreateAsync(new User { Email = "mark@gmail.com", Username = "mark" }).Result
            });

            AddBid(_id, new Bid
            {
                User = userStore.CreateAsync(new User { Email = "terry@gmail.com", Username = "terry" }).Result,
                Amount = new Currency("$300.00"),
                Timestamp = DateTime.Now.AddMinutes(1)
            });

            AddBid(_id, new Bid
            {
                User = userStore.CreateAsync(new User { Email = "alex@gmail.com", Username = "alex" }).Result,
                Amount = new Currency("$320.00"),
                Timestamp = DateTime.Now.AddMinutes(1)
            });
        }
    }
}
