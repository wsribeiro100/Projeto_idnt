using System;

namespace IDNT.TravelRoutes.Entities
{
    public class TRoute
    {
        private static int _count = 0;

        private String _origem;
        private String _destino;
        private int _valor;
        private int _id;

        public int Id { get { return _id; } }
        public String Origem { get { return _origem.ToUpper(); } }
        public String Destino { get { return _destino.ToUpper(); } }
        public int Valor { get { return _valor; } }

        public TRoute(String origem, String destino, int valor)
        {   
            CreateRoute(0, origem, destino, valor);
        }

        public TRoute(int id, String origem, String destino, int valor) {
            
            CreateRoute(id, origem, destino, valor);
        }

        private void CreateRoute(int id, String origem, String destino, int valor)
        {   
            _id = id;
            if (id == 0)
            {
                _count++;
                _id = _count;
            }

            if (String.IsNullOrEmpty(origem))
            {
                throw new Exception("Origem é obrigatório");
            }

            if (String.IsNullOrEmpty(destino))
            {
                throw new Exception("Destino é obrigatório");
            }

            if (origem == destino)
            {
                throw new Exception("Origem e Destino não podem ser iguais");
            }

            AddValor(valor);
           
            this._origem = origem.ToUpper();
            this._destino = destino.ToUpper();
        }

        public void AddValor(int valor) {
            if (valor <= 0)
            {
                throw new Exception("Valor é obrigatório");
            }

            this._valor = valor;
        }
    }
}
