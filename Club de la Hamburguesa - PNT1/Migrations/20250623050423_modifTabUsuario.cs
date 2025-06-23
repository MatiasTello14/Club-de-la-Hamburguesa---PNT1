using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Club_de_la_Hamburguesa___PNT1.Migrations
{
    /// <inheritdoc />
    public partial class modifTabUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescuentoBienvenidaAplicado",
                table: "Usuarios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DescuentoBienvenidaAplicado",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
