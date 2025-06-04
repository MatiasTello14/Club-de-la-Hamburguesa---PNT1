namespace Club_de_la_Hamburguesa___PNT1.Models
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public Tarjeta Tarjeta { get; set; }
        public bool DescuentoBienvenidaAplicado { get; set; }
    }
}
