using MenuComidaMVC.Models;

namespace MenuComidaMVC.Services
{
    
    public class PedidoService
    {
        private readonly List<Pedido> _pedidos = new();
        private readonly object _bloqueo = new();
        private int _siguienteId = 1;

        public List<Pedido> ObtenerTodos()
        {
            lock (_bloqueo)
            {
                return _pedidos.OrderByDescending(p => p.Fecha).ToList();
            }
        }

        public Pedido? ObtenerPorId(int id)
        {
            lock (_bloqueo)
            {
                return _pedidos.FirstOrDefault(p => p.Id == id);
            }
        }

        public Pedido Crear(string nombreCliente, List<ItemPedido> items)
        {
            lock (_bloqueo)
            {
                var pedido = new Pedido
                {
                    Id = _siguienteId++,
                    NombreCliente = nombreCliente,
                    Fecha = DateTime.Now,
                    Items = items
                };

                _pedidos.Add(pedido);

                return pedido;
            }
        }

        public bool CambiarEstado(int id, string estado)
        {
            lock (_bloqueo)
            {
                var pedido = _pedidos.FirstOrDefault(p => p.Id == id);

                if (pedido is null)
                {
                    return false;
                }

                pedido.Estado = estado;

                return true;
            }
        }
    }
}
