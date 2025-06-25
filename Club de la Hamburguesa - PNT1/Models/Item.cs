using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Club_de_la_Hamburguesa___PNT1.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public int HamburguesaId { get; set; }
        public Hamburguesa Hamburguesa { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }



        public Item() { }

        public Item(int cantidad, int hamburguesaId)
        {
            Cantidad = cantidad;
            HamburguesaId = hamburguesaId;

        }
    }
}
