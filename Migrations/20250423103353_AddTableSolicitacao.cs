using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class AddTableSolicitacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataNascimento",
                table: "Usuarios",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "SolicitacoesDoacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeOng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataSolicitacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCestaCompleta = table.Column<bool>(type: "bit", nullable: false),
                    QtdeCestas = table.Column<int>(type: "int", nullable: false),
                    ItensSolicitados = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoTransporte = table.Column<int>(type: "int", nullable: false),
                    DataConclusao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitacoesDoacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpcoesAgendamentoSolicit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiaSemana = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HorarioInicio = table.Column<TimeSpan>(type: "time", nullable: false),
                    HorarioFim = table.Column<TimeSpan>(type: "time", nullable: false),
                    SolicitacaoDoacaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpcoesAgendamentoSolicit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpcoesAgendamentoSolicit_SolicitacoesDoacao_SolicitacaoDoacaoId",
                        column: x => x.SolicitacaoDoacaoId,
                        principalTable: "SolicitacoesDoacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpcoesAgendamentoSolicit_SolicitacaoDoacaoId",
                table: "OpcoesAgendamentoSolicit",
                column: "SolicitacaoDoacaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpcoesAgendamentoSolicit");

            migrationBuilder.DropTable(
                name: "SolicitacoesDoacao");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "DataNascimento",
                table: "Usuarios",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
