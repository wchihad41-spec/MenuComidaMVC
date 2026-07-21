namespace MenuComidaMVC.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        public string NombreCliente { get; set; } = string.Empty;

        public DateTime Fecha { get; set; } = DateTime.Now;

        public List<ItemPedido> Items { get; set; } = new();

        public string Estado { get; set; } = "Pendiente";

        public decimal Total => Items.Sum(i => i.Subtotal);
    }
}
