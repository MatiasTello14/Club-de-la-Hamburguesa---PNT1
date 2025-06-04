using System.ComponentModel.DataAnnotations;

namespace Club_de_la_Hamburguesa___PNT1.Models
{
    public class Hamburguesa
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public List<Ingrediente> Ingredientes { get; set; }

        [EnumDataType(typeof(Bebida))]
        public Bebida BebidaCombo { get; set; }
    }
}
