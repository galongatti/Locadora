using Microsoft.EntityFrameworkCore.Migrations;

namespace Locadora.Migrations
{
    public partial class terceira_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataDevolucao",
                table: "Locacao",
                newName: "DataParaDevolucao");

            migrationBuilder.AddColumn<int>(
                name: "DiasAlocacao",
                table: "Locacao",
                type: "INT",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiasAlocacao",
                table: "Locacao");

            migrationBuilder.RenameColumn(
                name: "DataParaDevolucao",
                table: "Locacao",
                newName: "DataDevolucao");
        }
    }
}
