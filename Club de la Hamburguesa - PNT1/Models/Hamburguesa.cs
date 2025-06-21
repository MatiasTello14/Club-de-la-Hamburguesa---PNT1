using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Club_de_la_Hamburguesa___PNT1.Models
{
    public class Hamburguesa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ImagenUrl { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }


        public Hamburguesa() { }

        public Hamburguesa(string nombre, string descripcion, double precio, int stock, string imagenUrl)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
            Stock = stock;
            ImagenUrl = imagenUrl; 
        }



    }


}
