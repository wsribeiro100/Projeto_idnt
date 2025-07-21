using Tr = IDNT.TravelRoutes.Entities;

namespace IDNT.API.DTO
{
    public class RouteDTO
    {
        public int Id { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public int Valor { get; set; }

        public Tr.TRoute getRoute()
        {
            return new Tr.TRoute(this.Id, this.Origem.ToUpper(), this.Destino.ToUpper(), this.Valor);
        }
    }
}
