using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Club_de_la_Hamburguesa___PNT1.Migrations
{
    /// <inheritdoc />
    public partial class CambiosAtributosEnPedidoEItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BebidaElegida",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IngredienteAdicional",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "BebidaElegida",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IngredienteAdicional",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BebidaElegida",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "IngredienteAdicional",
                table: "Pedidos");

            migrationBuilder.AddColumn<int>(
                name: "BebidaElegida",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IngredienteAdicional",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
