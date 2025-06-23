using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class CriarTabelaTransacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItensSolicitados",
                table: "SolicitacaoDoacao");

            migrationBuilder.CreateTable(
                name: "ItemSolicitacao",
                columns: table => new
                {
                    ClientId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alimento = table.Column<int>(type: "int", nullable: false),
                    QtdeItem = table.Column<int>(type: "int", nullable: false),
                    MedidaItem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Transacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDoador = table.Column<int>(type: "int", nullable: false),
                    IdOng = table.Column<int>(type: "int", nullable: false),
                    IdOferta = table.Column<int>(type: "int", nullable: false),
                    IdSolicitacao = table.Column<int>(type: "int", nullable: false),
                    TipoDoacao = table.Column<int>(type: "int", nullable: false),
                    TipoTransporte = table.Column<int>(type: "int", nullable: false),
                    DataAgendada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataConclusao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transacao_OfertaDoacao_IdOferta",
                        column: x => x.IdOferta,
                        principalTable: "OfertaDoacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transacao_SolicitacaoDoacao_IdSolicitacao",
                        column: x => x.IdSolicitacao,
                        principalTable: "SolicitacaoDoacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transacao_Usuario_IdDoador",
                        column: x => x.IdDoador,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Transacao_Usuario_IdOng",
                        column: x => x.IdOng,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transacao_IdDoador",
                table: "Transacao",
                column: "IdDoador");

            migrationBuilder.CreateIndex(
                name: "IX_Transacao_IdOferta",
                table: "Transacao",
                column: "IdOferta");

            migrationBuilder.CreateIndex(
                name: "IX_Transacao_IdOng",
                table: "Transacao",
                column: "IdOng");

            migrationBuilder.CreateIndex(
                name: "IX_Transacao_IdSolicitacao",
                table: "Transacao",
                column: "IdSolicitacao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemSolicitacao");

            migrationBuilder.DropTable(
                name: "Transacao");

            migrationBuilder.AddColumn<string>(
                name: "ItensSolicitados",
                table: "SolicitacaoDoacao",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
