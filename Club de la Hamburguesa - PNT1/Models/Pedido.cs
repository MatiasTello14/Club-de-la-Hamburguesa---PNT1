using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Club_de_la_Hamburguesa___PNT1.Models
{
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Entrega MetodoEntrega { get; set; }
        public Pago MetodoPago { get; set; }
        public string Direccion { get; set; }
        public Estado Estado { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public ICollection<Item> Items { get; set; }




        public Pedido() {
            Items = new List<Item>();
        }

        public Pedido(Entrega metodoEntrega, Pago metodoPago, string direccion, int usuarioId)
        {
            MetodoEntrega = metodoEntrega;
            MetodoPago = metodoPago;
            Direccion = direccion;
            UsuarioId = usuarioId;
            Estado = Estado.EnPreparacion;
            Items = new List<Item>();
        }
    }
}
