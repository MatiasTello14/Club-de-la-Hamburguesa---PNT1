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
        public Tarjeta? Tarjeta { get; set; }

        [Display(Name = "Descuento de bienvenida aplicado")]
        public bool DescuentoBienvenidaAplicado { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }
        public bool EsAdmin { get; set; }


        public Usuario() {
            Pedidos = new List<Pedido>();
        }

        public Usuario(string nombre, string email, string contraseña)
        {
            setNombre(nombre);
            setEmail(email);
            setContraseña(contraseña);
            DescuentoBienvenidaAplicado = false;
            Pedidos = new List<Pedido>();
            EsAdmin = false;
        }

        public void setNombre(string n)
        {
            if (n == null || n == "")
            {
                throw new Exception("El nombre no puede estar vacío.");
            }
            Nombre = n;
        }

        public void setEmail(string e)
        {
            if (e == null || e == "" || !e.Contains("@") || !e.EndsWith(".com"))
            {
                throw new Exception("El email debe contener '@' y terminar en '.com'.");
            }

            Email = e;
        }

        public void setContraseña(string c)
        {
            if (c == null || c.Length < 6)
            {
                throw new Exception("La contraseña debe tener al menos 6 caracteres.");
            }

            bool tieneMayuscula = false;
            int i = 0;

            while (i < c.Length && !tieneMayuscula)
            {
                if (char.IsUpper(c[i]))
                {
                    tieneMayuscula = true;
                }
                i++;
            }

            if (!tieneMayuscula)
            {
                throw new Exception("La contraseña debe tener al menos una letra mayúscula.");
            }

            Contraseña = c;
        }

    }
}

