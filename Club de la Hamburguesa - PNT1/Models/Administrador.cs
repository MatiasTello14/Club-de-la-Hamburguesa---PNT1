namespace Club_de_la_Hamburguesa___PNT1.Models
{
    public class Administrador
    {
        public int Id { get; set; }
        public string Nombre { get; set; }




        public Administrador() { }

        public Administrador(string nombre)
        {
            Nombre = nombre;
        }
        public void CambiarEstadoPedido(Pedido pedido, Estado nuevoEstado)
        {
            pedido.Estado = nuevoEstado;
        
        }
    }
}
