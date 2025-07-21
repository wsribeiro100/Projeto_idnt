using IDNT.TravelRoutes.Entities;

namespace IDNT.TravelRoutes.Services
{
    public interface ITravelRouteService
    {
        TRoute Create(TRoute route);
        TRoute Delete(int id);
        void Update(TRoute route);
        List<TRoute> GetRoutes();
        (int preco, string caminho) GetBetterRoute(string origem, string destino);
    }
}
