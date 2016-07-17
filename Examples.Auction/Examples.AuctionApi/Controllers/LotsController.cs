using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;
using Examples.AuctionApi.Infrastructure;
using Examples.AuctionApi.Models;

namespace Examples.AuctionApi.Controllers
{
    public class LotsController : ApiController
    {
        private readonly ILotStore _store;

        public LotsController(ILotStore store)
        {
            _store = store;
        }

        public async Task<HttpResponseMessage> Get(int id)
        {
            var lot = await _store.FindAsync(id);

            return lot == null
                ? Request.CreateResponse(HttpStatusCode.NotFound, "Invalid ID")
                : Request.CreateResponse(HttpStatusCode.OK, lot);
        }

        public async Task<HttpResponseMessage> Post(Lot lot)
        {
            await _store.CreateAsync(lot);
            var response = Request.CreateResponse(HttpStatusCode.Created, lot);

            var urlHelper = new UrlHelper(Request);
            var controllerName = this.GetType().Name;
            controllerName = controllerName.Substring(0, controllerName.Length - "controller".Length).ToLower();

            response.Headers.Location = new Uri(urlHelper.Link(RouteConfig.DefaultApi, new { controller = controllerName, id = lot.Id }));

            return response;
        }
    }
}
