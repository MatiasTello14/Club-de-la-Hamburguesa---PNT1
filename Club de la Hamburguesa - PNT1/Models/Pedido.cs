using System.ComponentModel.DataAnnotations;

namespace Club_de_la_Hamburguesa___PNT1.Models
{
    public class Pedido
    {
        public Entrega metodoEntrega { get; set; }

        public Pago metodoPago { get; set; }

        public string direccion { get; set; }

        public Estado estado { get; set; }

    }
}
