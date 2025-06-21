using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Club_de_la_Hamburguesa___PNT1.Models
{
    public class Tarjeta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String Numero { get; set; }
        public string Titular { get; set; }
        public int AnioVencimiento { get; set; }
        public int CodigoSeguridad { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public Tarjeta() { }

        public Tarjeta(String numero, string titular, int anio, int codigo)
        {
            setNumero(numero);
            setTitular(titular);
            setAnioVencimiento(anio);
            setCodigoSeguridad(codigo);
        }

        public void setNumero(String n)
        {

            if (n.Length < 13 || n.Length > 19)
            {
                throw new Exception("El número de tarjeta debe tener entre 13 y 19 dígitos.");
            }
            if (!n.All(char.IsDigit)) { 
                throw new Exception("El número de tarjeta solo debe contener dígitos.");
            }
            Numero = n;
        }

        public void setTitular(String t)
        {
            if (t == null || t == "")
            {
                throw new Exception( "El titular ingresado es invalido");
            }
            else
            {
                Titular = t;
            }
        }

        public void setAnioVencimiento(int a)
        {

            if (a < DateTime.Now.Year)
            {
                throw new Exception ("El año ingresado es invalido");
            }
            else
            {
                AnioVencimiento = a;
            }
        }

        public void setCodigoSeguridad(int n)
        {

            if (n < 100 || n > 9999)
            {
                throw new Exception("El codigo ingresado es invalido.");
            }
            else
            {
                CodigoSeguridad = n;
            }
        }
    }
}
