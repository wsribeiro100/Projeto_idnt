using IDNT.API.DTO;
using IDNT.API.Services;
using IDNT.TravelRoutes.Entities;
using IDNT.TravelRoutes.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace IDNT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelRouteController : ControllerBase
    {
        APITravelRouteService _apiTravelRouteService;

        public TravelRouteController(ITravelRouteService travelRouteService, IDistributedCache cache)
        {
            _apiTravelRouteService = new APITravelRouteService(travelRouteService, cache);
        }

        [HttpGet]
        public List<TRoute> Get()
        {   
            return _apiTravelRouteService.GetRoutes();
        }

        [HttpGet("{origem}/{destino}")]
        public async Task<string> Get(string origem, string destino)
        {
            return await _apiTravelRouteService.GetBetterRoute(origem, destino);
        }

        [HttpPost]
        public ActionResult<RouteDTO> Create([FromBody] RouteDTO routeDTO)
        {
            try
            {
                return Ok(_apiTravelRouteService.Create(routeDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public ActionResult<RouteDTO> Put([FromBody] RouteDTO routeDTO)
        {
            try
            {
                return Ok(_apiTravelRouteService.Update(routeDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                return Ok(_apiTravelRouteService.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }           
        }
    }
}
