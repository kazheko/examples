using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Examples.AuctionApi.Controllers;
using Examples.AuctionApi.Infrastructure;
using Examples.AuctionApi.Models;
using NSubstitute;
using Xunit;

namespace Examples.AuctionApi.UnitTests.Controllers
{

    public class LotsControllerTests
    {
        private readonly LotsController _controller;
        private readonly ILotStore _lotStore;

        public LotsControllerTests()
        {
            _lotStore = Substitute.For<ILotStore>();
            _controller = new LotsController(_lotStore);
        }

        [Fact]
        public void ShouldReturnLotWhenGETForExistingLot()
        {
            var lot = new Lot {Id = 1};
            
            _lotStore.FindAsync(1).Returns(Task.FromResult(lot));

            var foundLot = _controller.Get(1).Result;

            Assert.Equal(lot, foundLot);
        }
        [Fact]
        public void ShouldReturnNotFoundWhenGETForNonExistingIssue()
        {
            _lotStore.FindAsync(1).Returns(Task.FromResult((Lot)null)); // <1>

            var ex = Assert.Throws<AggregateException>(() =>
            {
                var task = _controller.Get(1);
                var result = task.Result;
            }); // <2>

            Assert.IsType<HttpResponseException>(ex.InnerException); // <3>
            Assert.Equal(HttpStatusCode.NotFound,
              ((HttpResponseException)ex.InnerException).Response.StatusCode); // <4>
        }
    }
}
