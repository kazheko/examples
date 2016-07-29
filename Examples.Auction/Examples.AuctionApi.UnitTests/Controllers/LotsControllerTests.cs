using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
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
        public void ShouldReturnNotFoundWhenGETForNonExistingLot()
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

        [Fact]
        public void ShouldReturnAllLotsWhenGet()
        {
            IEnumerable<Lot> lots = new[] {new Lot(), new Lot()};

            _lotStore.FindAsync().Returns(Task.FromResult(lots));

            var foundLots = _controller.Get().Result;

            Assert.Equal(lots,foundLots);
        }

        [Fact]
        public void ShouldCallCreateAsyncWhenPOSTForNewLot()
        {
            //Arrange
            ConfigureForTesting(_controller, HttpMethod.Post, "http://test.com/lots"); // <1>
            var createdLot = new Lot { Id = 1 };

            //Act
            var response = _controller.Post(createdLot).Result; // <3>

            //Assert
            _lotStore.ReceivedWithAnyArgs().CreateAsync(Arg.Any<Lot>()); // <4> 
        }

        [Fact]
        public void ShouldSetResponseHeadersWhenPOSTForNewLot()
        {
            //Arrange
            ConfigureForTesting(_controller, HttpMethod.Post, "http://test.com/lots");
            var createdLot = new Lot {Id = 1};
            _lotStore.CreateAsync(createdLot).Returns(Task.FromResult(createdLot)); // <1>
            
            //Act
            var response = _controller.Post(createdLot).Result; // <2>
            
            //Assert
            Assert.Equal(response.StatusCode, HttpStatusCode.Created); // <3>
            Assert.Equal(response.Headers.Location.AbsoluteUri, "http://test.com/lots/1");
        }

        private static void ConfigureForTesting(ApiController controller, HttpMethod method, string url)
        {
            controller.Configuration = new HttpConfiguration(); // <1>
            var route = controller.Configuration.Routes.MapHttpRoute(
              name: "DefaultApi",
              routeTemplate: "{controller}/{id}",
              defaults: new { id = RouteParameter.Optional }
            ); // <2>

            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "Issues" } });
            controller.Request = new HttpRequestMessage(method, url);  // <3>
            controller.Request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, controller.Configuration);
            controller.Request.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData); // <4>
        }


    }
}
