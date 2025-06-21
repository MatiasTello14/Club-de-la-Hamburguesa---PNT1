using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Club_de_la_Hamburguesa___PNT1.Models
{
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [EnumDataType(typeof(Entrega))]
        public Entrega MetodoEntrega { get; set; }

        [EnumDataType(typeof(Pago))]
        public Pago MetodoPago { get; set; }
        public string ? Direccion { get; set; }
        public Estado Estado { get; set; }
        public int UsuarioId { get; set; }
        [ValidateNever]
        public Usuario Usuario { get; set; }

        [EnumDataType(typeof(Bebida))]
        public Bebida BebidaElegida { get; set; }

        [EnumDataType(typeof(Ingrediente))]
        public Ingrediente IngredienteAdicional { get; set; }
        public DateTime Fecha { get; set; }
        public ICollection<Item> Items { get; set; }





        public Pedido() {
            Items = new List<Item>();
        }

        public Pedido(Entrega metodoEntrega, Pago metodoPago, string direccion, int usuarioId, Bebida bebida, Ingrediente ingrediente)
        {
            MetodoEntrega = metodoEntrega;
            MetodoPago = metodoPago;
            Direccion = direccion;
            UsuarioId = usuarioId;
            Estado = Estado.EnPreparacion;
            BebidaElegida = bebida;
            IngredienteAdicional = ingrediente;
            Items = new List<Item>();
        }

        public double ObtenerMontoTotal()
        {
            double totalHamburguesas = Items.Sum(i => i.Hamburguesa?.Precio ?? 0);
            double totalExtras = (int)BebidaElegida + (int)IngredienteAdicional;
            return totalHamburguesas + totalExtras;
        }
    }


}
