using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Club_de_la_Hamburguesa___PNT1.Migrations
{
    /// <inheritdoc />
    public partial class nuevasModifEnTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BebidaCombo",
                table: "Hamburguesas");

            migrationBuilder.DropColumn(
                name: "Ingredientes",
                table: "Hamburguesas");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BebidaElegida",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IngredienteAdicional",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "BebidaCombo",
                table: "Hamburguesas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Ingredientes",
                table: "Hamburguesas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
