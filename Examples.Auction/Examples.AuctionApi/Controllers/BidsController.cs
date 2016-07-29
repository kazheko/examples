using System;
using Examples.AuctionApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Examples.AuctionApi.Infrastructure;

namespace Examples.AuctionApi.Controllers
{
    public class BidsController : ApiController
    {
        private readonly ILotStore _lotStore;
        private readonly IUserStore _userStore;
        public BidsController(ILotStore lotStore, IUserStore userStore)
        {
            _lotStore = lotStore;
            _userStore = userStore;
        }
        public async Task<IEnumerable<Bid>> GetByLotId(int lotId)
        {
            var lot = await _lotStore.FindAsync(lotId);

            if(lot == null) throw new HttpResponseException(HttpStatusCode.NotFound);

            return lot.Bids.AsEnumerable();
        }

        [Authorize]
        public async Task<HttpResponseMessage> Post(int lotId, string currency)
        {
            var lot = await _lotStore.FindAsync(lotId);

            if (lot == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            
            var email = Thread.CurrentPrincipal.Identity.Name;
            var user = await _userStore.FindAsync(email);

            try
            {
                await _lotStore.AddBid(lotId, new Bid
                {
                    Amount = new Currency(currency),
                    Timestamp = DateTime.Now,
                    User = user
                });
            }
            catch (ArgumentException)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
