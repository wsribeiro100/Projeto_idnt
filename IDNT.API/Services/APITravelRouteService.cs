using IDNT.API.DTO;
using IDNT.TravelRoutes.Entities;
using IDNT.TravelRoutes.Services;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace IDNT.API.Services
{
    public class APITravelRouteService
    {
        private ITravelRouteService _travelRouteService;
        private readonly IDistributedCache _cache;
        public APITravelRouteService(ITravelRouteService travelRouteService, IDistributedCache cache) {
            this._travelRouteService = travelRouteService;
            this._cache = cache;
        }

        public List<TRoute> GetRoutes()
        {
            return _travelRouteService.GetRoutes();
        }

        public async Task<string> GetBetterRoute(string origem, string destinoFinal)
        {
            string key = GetKeyCache(origem, destinoFinal);

            var valor = await _cache.GetAsync(key);

            if (valor != null)
            {   
                return Encoding.UTF8.GetString(valor);
            }

            var result = _travelRouteService.GetBetterRoute(origem, destinoFinal);

            if (result.preco > 0)
            {
                string msg = $"{result.caminho} ao custo de {result.preco}";

                await _cache.SetAsync(key, Encoding.UTF8.GetBytes(msg), new DistributedCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                });

                return msg;
            }

            return $"{result.caminho}";
        }

        public RouteDTO Create(RouteDTO routeDto)
        {   
            var r = _travelRouteService.Create(routeDto.getRoute());
            routeDto.Id = r.Id;
            return routeDto;
        }

        public TRoute Delete(int id) {

            var route = _travelRouteService.Delete(id);

            if (route is not null)
            {
                DeleteCache(new RouteDTO() { Origem = route.Origem, Destino = route.Destino });
            }

            return route;
        }

        public RouteDTO Update(RouteDTO routeDto)
        {
            _travelRouteService.Update(routeDto.getRoute());

            DeleteCache(routeDto);

            return routeDto;
        }

        private async void DeleteCache(RouteDTO routeDto)
        {
            string key = GetKeyCache(routeDto);
            await _cache.RemoveAsync(key);
        }

        private string GetKeyCache(RouteDTO routeDto)
        {
            return GetKeyCache(routeDto.Origem, routeDto.Destino);
        }

        private string GetKeyCache(string origem, string destinoFinal)
        {
            return $"{origem}-{destinoFinal}";
        }
    }
}
