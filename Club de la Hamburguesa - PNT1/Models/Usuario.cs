using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Club_de_la_Hamburguesa___PNT1.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }

        [Display(Name = "Tarjeta de credito")]
        public Tarjeta Tarjeta { get; set; }

        [Display(Name = "Descuento de bienvenida aplicado")]
        public bool DescuentoBienvenidaAplicado { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }



        public Usuario() { }

        public Usuario(string nombre, string email, string contraseña)
        {
            Nombre = nombre;
            Email = email;
            Contraseña = contraseña;
            DescuentoBienvenidaAplicado = false;
        }
    }
}

