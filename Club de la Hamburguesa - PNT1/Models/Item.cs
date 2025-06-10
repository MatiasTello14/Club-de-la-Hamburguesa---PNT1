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
        public Hamburguesa HamburguesaCombo { get; set; }

        [EnumDataType(typeof(Bebida))]
        public Bebida BebidaElegida { get; set; }

        [EnumDataType(typeof(Ingrediente))]
        public Ingrediente IngredienteAdicional { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }



        public Item() { }

        public Item(int cantidad, int hamburguesaId, Bebida bebida, Ingrediente ingrediente)
        {
            Cantidad = cantidad;
            HamburguesaId = hamburguesaId;
            BebidaElegida = bebida;
            IngredienteAdicional = ingrediente;
        }

        public double CalcularPrecioTotalItem(Item item)
        {
            return 0;
        }
    }
}
