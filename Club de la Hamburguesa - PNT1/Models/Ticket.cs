namespace Club_de_la_Hamburguesa___PNT1.Models
{
    public class Ticket
    {

        public int Id { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }


        public Ticket() { 
        }
        public void Imprimir()
        {

        }
    }
}
