using System.ComponentModel.DataAnnotations;

namespace Club_de_la_Hamburguesa___PNT1.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public Entrega MetodoEntrega { get; set; }

        public Pago MetodoPago { get; set; }

        public string Direccion { get; set; }

        public Estado Estado { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public DateTime Fecha { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
