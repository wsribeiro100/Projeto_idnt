using IDNT.API.Controllers;
using IDNT.API.DTO;
using IDNT.TravelRoutes.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using UnitTests;

namespace IDNT.API.Tests.Controllers
{
    public class TravelRouteControllerTests : IClassFixture<DistributedCacheFixture>
    {
        private readonly IDistributedCache _cache;

        public TravelRouteControllerTests(DistributedCacheFixture fixture)
        {
            _cache = fixture.Cache;
        }

        [Fact]
        public async void CreateRoute()
        {
            var travelRouteServiceMock = new Mock<TravelRouteService>();
            var controller = new TravelRouteController(travelRouteServiceMock.Object, _cache);

            var request = new RouteDTO
            {
                Origem = "CGB",
                Destino = "BSB",
                Valor = 50
            };

            ActionResult<RouteDTO> result = controller.Create(request);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var route = Assert.IsType<RouteDTO>(okResult.Value);

            Assert.Equal(
                new { request.Origem, request.Destino, request.Valor },
                new { route.Origem, route.Destino, route.Valor }
            );
        }

        [Fact]
        public async void UpdateRoute()
        {
            var travelRouteServiceMock = new Mock<TravelRouteService>();
            var controller = new TravelRouteController(travelRouteServiceMock.Object, _cache);

            var request = new RouteDTO
            {
                Id = 5,
                Origem = "GRU",
                Destino = "ORL",
                Valor = 50
            };

            ActionResult<RouteDTO> result = controller.Put(request);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var route = Assert.IsType<RouteDTO>(okResult.Value);

            Assert.Equal(
                new { request.Origem, request.Destino, request.Valor },
                new { route.Origem, route.Destino, route.Valor }
            );
        }

        [Fact]
        public async void GetAllRoute()
        {
            var travelRouteServiceMock = new Mock<TravelRouteService>();
            var controller = new TravelRouteController(travelRouteServiceMock.Object, _cache);

            var result = controller.Get();

            Assert.NotNull(result);
            Assert.True(result.Count > 5);
        }

        [Fact]
        public async void DeleteRoute()
        {
            var travelRouteServiceMock = new Mock<TravelRouteService>();
            var controller = new TravelRouteController(travelRouteServiceMock.Object, _cache);

            var result = controller.Delete(5);

            Assert.NotNull(result);
        }

        [Fact]
        public async void GetBetterRoute()
        {
            var travelRouteServiceMock = new Mock<TravelRouteService>();
            var controller = new TravelRouteController(travelRouteServiceMock.Object, _cache);

            var result = await controller.Get("GRU", "CGD");

            Assert.NotNull(result);
            Assert.Equal("GRU - BRC - SCL - ORL - CGD ao custo de 40", result);
        }
    }
}
