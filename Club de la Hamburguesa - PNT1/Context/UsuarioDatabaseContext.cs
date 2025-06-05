using Club_de_la_Hamburguesa___PNT1.Models;
using Microsoft.EntityFrameworkCore;

namespace Club_de_la_Hamburguesa___PNT1.Context
{
    public class UsuarioDatabaseContext : DbContext
    {

        public UsuarioDatabaseContext(DbContextOptions<UsuarioDatabaseContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
