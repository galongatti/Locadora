using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Locadora.Migrations
{
    public partial class primeira_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Documento = table.Column<string>(type: "VARCHAR(20)", nullable: true),
                    Ativo = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filme",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Ativo = table.Column<bool>(type: "BIT", nullable: false),
                    Disponivel = table.Column<bool>(type: "BIT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filme", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDCliente = table.Column<int>(type: "int", nullable: false),
                    Situacao = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    DataAlocacao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DataParaDevolucao = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    DiasAlocacao = table.Column<int>(type: "INT", nullable: false),
                    ObservacaoSituacao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locacao_Cliente_IDCliente",
                        column: x => x.IDCliente,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocacaoItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDFilme = table.Column<int>(type: "int", nullable: false),
                    IDLocacao = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocacaoItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocacaoItem_Filme_IDFilme",
                        column: x => x.IDFilme,
                        principalTable: "Filme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocacaoItem_Locacao_IDLocacao",
                        column: x => x.IDLocacao,
                        principalTable: "Locacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locacao_IDCliente",
                table: "Locacao",
                column: "IDCliente");

            migrationBuilder.CreateIndex(
                name: "IX_LocacaoItem_IDFilme",
                table: "LocacaoItem",
                column: "IDFilme");

            migrationBuilder.CreateIndex(
                name: "IX_LocacaoItem_IDLocacao",
                table: "LocacaoItem",
                column: "IDLocacao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocacaoItem");

            migrationBuilder.DropTable(
                name: "Filme");

            migrationBuilder.DropTable(
                name: "Locacao");

            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
