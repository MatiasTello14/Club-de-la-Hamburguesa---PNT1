using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Club_de_la_Hamburguesa___PNT1.Models
{
    public class Tarjeta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Titular { get; set; }
        public int AnioVencimiento { get; set; }
        public int CodigoSeguridad { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public Tarjeta() { }

        public Tarjeta(int numero, string titular, int anio, int codigo)
        {
            Numero = numero;
            Titular = titular;
            AnioVencimiento = anio;
            CodigoSeguridad = codigo;
        }
    }
}
