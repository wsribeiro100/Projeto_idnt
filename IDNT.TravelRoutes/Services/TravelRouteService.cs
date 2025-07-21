
using IDNT.TravelRoutes.Entities;

namespace IDNT.TravelRoutes.Services
{
    public class TravelRouteService : ITravelRouteService
    {
        private static List<TRoute> _routes = new List<TRoute>();
        private static Dictionary<string, List<(string Destino, int valor)>> grafo;     

        public TravelRouteService()
        {
            CreateRoutes();
        }
       
        private void CreateRoutes()
        {
            if (_routes.Count > 0)
                return;

            _routes.Add(new TRoute("GRU", "BRC", 10));
            _routes.Add(new TRoute("BRC", "SCL", 5));
            _routes.Add(new TRoute("GRU", "CGD", 75));
            _routes.Add(new TRoute("GRU", "SCL", 20));
            _routes.Add(new TRoute("GRU", "ORL", 56));
            _routes.Add(new TRoute("ORL", "CGD", 5));
            _routes.Add(new TRoute("SCL", "ORL", 20));
            
            _routes.Add(new TRoute("CGD", "ORL", 25));
            _routes.Add(new TRoute("CGB", "GRU", 35));
            _routes.Add(new TRoute("CGB", "FOR", 8));
            _routes.Add(new TRoute("GRU", "FOR", 3));
            _routes.Add(new TRoute("FOR", "CGB", 2));

            CreateGrafo();
        }

        private static void CreateGrafo()
        {
            grafo = new Dictionary<string, List<(string Destino, int valor)>>();

            foreach (var route in _routes)
            {
                AddGrafo(route);
            }
        }

        private static void AddGrafo(TRoute route)
        {
            if (!grafo.ContainsKey(route.Origem))
            {
                grafo[route.Origem] = new List<(string Destino, int valor)>();
            }
            grafo[route.Origem].Add((route.Destino, route.Valor));
        }

        private static void RemoveGrafo(TRoute route)
        {
            if (grafo.ContainsKey(route.Origem))
            {
                grafo[route.Origem].RemoveAll(r => r.Destino.ToUpper() == route.Destino);
            }

            if (grafo[route.Origem].Count == 0)
            {
                grafo.Remove(route.Origem);
            }
        }

        public (int preco, string caminho) GetBetterRoute(string origem, string destinoFinal)
        {
            var distancias = new Dictionary<string, int>();
            var anteriores = new Dictionary<string, string>();
            var visitados = new HashSet<string>();
            var fila = new HashSet<string>();

            foreach (var no in grafo.Keys)
            {
                distancias[no] = int.MaxValue;
                anteriores[no] = null;
                fila.Add(no);
            }

            distancias[origem] = 0;

            while (fila.Count > 0)
            {
                string noAtual = fila.OrderBy(no => distancias.ContainsKey(no) ? distancias[no] : int.MaxValue).First();

                fila.Remove(noAtual);
                visitados.Add(noAtual);

                if (!grafo.ContainsKey(noAtual))
                    continue;

                foreach (var filho in grafo[noAtual])
                {
                    if (visitados.Contains(filho.Destino))
                        continue;

                    int novaDistancia = distancias[noAtual] + filho.valor;

                    if (!distancias.ContainsKey(filho.Destino) || novaDistancia < distancias[filho.Destino])
                    {
                        distancias[filho.Destino] = novaDistancia;
                        anteriores[filho.Destino] = noAtual;
                        fila.Add(filho.Destino);
                    }
                }
            }

            return FormatResult(destinoFinal, anteriores, distancias);
        }

        private (int preco, string caminho) FormatResult(string destinoFinal, Dictionary<string, string> anteriores, Dictionary<string, int> distancias)
        {
            var caminho = new List<string>();
            string atual = destinoFinal;
            string separador = " - ";

            while (atual != null)
            {
                caminho.Insert(0, atual);
                anteriores.TryGetValue(atual, out atual);
            }

            int custoFinal = distancias.ContainsKey(destinoFinal) ? distancias[destinoFinal] : -1;

            if (custoFinal == int.MaxValue || custoFinal < 0)
            {
                custoFinal = 0;
                caminho = new List<string>();
                caminho.Insert(0, "Rota não encontrada");
                separador = String.Empty;
            }

            return (custoFinal, string.Join(separador, caminho));
        }

        public TRoute Create(TRoute route)
        {
            _routes.Add(route);
            AddGrafo(route);
            return route;
        }

        public TRoute Delete(int id)
        {
            var route = _routes.Find(x => x.Id == id);

            if (route is null) return null;

            _routes.Remove(route);

            RemoveGrafo(route);

            return route;
        }

        public void Update(TRoute route)
        {
            if (!ExistsRoute(route))
            {
                throw new Exception("Rota não encontrada");
            }

            TRoute _route = Delete(route.Id);

            _route.AddValor(route.Valor);

            Create(_route);
        }

        private bool ExistsRoute(TRoute route)
        {
            return _routes.Exists(r => r.Origem == route.Origem && r.Destino == route.Destino);
        }

        public List<TRoute> GetRoutes()
        {
            return _routes.OrderBy(r => r.Id).ToList();
        }
    }
}