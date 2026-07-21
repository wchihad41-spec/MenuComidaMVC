namespace MenuComidaMVC.Models
{
    public class ItemPedido
    {
        public string Nombre { get; set; } = string.Empty;

        public decimal Precio { get; set; }

        public int Cantidad { get; set; }

        public decimal Subtotal => Precio * Cantidad;
    }
}