using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loja.Migrations
{
    /// <inheritdoc />
    public partial class final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumNotaFiscal",
                table: "Vendas");

            migrationBuilder.RenameColumn(
                name: "dataVenda",
                table: "Vendas",
                newName: "DataVenda");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataVenda",
                table: "Vendas",
                newName: "dataVenda");

            migrationBuilder.AddColumn<int>(
                name: "NumNotaFiscal",
                table: "Vendas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
