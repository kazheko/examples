using System.Collections.Generic;
using System.Web.Http;

namespace Examples.AuctionApi.Controllers
{
    public class AuctionsController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new[] {"value1", "value2"};
        }
    }
}
