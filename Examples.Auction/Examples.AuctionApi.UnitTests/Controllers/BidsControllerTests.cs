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
    public class BidsControllerTests
    {
        private readonly BidsController _controller;
        private readonly ILotStore _lotStore;
        private readonly IUserStore _userStore;

        public BidsControllerTests()
        {
            _userStore = Substitute.For<IUserStore>();
            _lotStore = Substitute.For<ILotStore>();
            _controller = new BidsController(_lotStore, _userStore);
        }

        [Fact]
        public void ShouldReturnAllBidsWhenGETForExistingLot()
        {
            var lot = new Lot(new[] {new Bid(), new Bid()});

            _lotStore.FindAsync(Arg.Any<int>()).Returns(Task.FromResult(lot));

            var foundBids = _controller.GetByLotId(Arg.Any<int>()).Result;

            Assert.Equal(lot.Bids, foundBids);
        }
        [Fact]
        public void ShouldReturnNotFoundWhenGETForNonExistingLot()
        {
            _lotStore.FindAsync(Arg.Any<int>()).Returns(Task.FromResult((Lot)null)); // <1>

            var ex = Assert.Throws<AggregateException>(() =>
            {
                var task = _controller.GetByLotId(Arg.Any<int>());
                var result = task.Result;
            }); // <2>

            Assert.IsType<HttpResponseException>(ex.InnerException); // <3>
            Assert.Equal(HttpStatusCode.NotFound,
              ((HttpResponseException)ex.InnerException).Response.StatusCode); // <4>
        }

        [Fact]
        public void ShouldReturnNotFoundWhenPOSTForNonExistingLot()
        {
            _lotStore.FindAsync(Arg.Any<int>()).Returns(Task.FromResult((Lot)null)); // <1>

            var ex = Assert.Throws<AggregateException>(() =>
            {
                var task = _controller.Post(Arg.Any<int>(),Arg.Any<string>());
                var result = task.Result;
            }); // <2>

            Assert.IsType<HttpResponseException>(ex.InnerException); // <3>
            Assert.Equal(HttpStatusCode.NotFound,
              ((HttpResponseException)ex.InnerException).Response.StatusCode); // <4>
        }
    }
}
