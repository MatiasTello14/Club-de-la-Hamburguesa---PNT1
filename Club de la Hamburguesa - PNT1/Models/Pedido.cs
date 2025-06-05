using System.ComponentModel.DataAnnotations;

namespace Club_de_la_Hamburguesa___PNT1.Models
{
    public class Pedido
    {
        public Entrega MetodoEntrega { get; set; }

        public Pago MetodoPago { get; set; }

        public string Direccion { get; set; }

        public Estado Estado { get; set; }

    }
}
