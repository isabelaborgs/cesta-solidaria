using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class AddOfertaDoacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Complemento",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "OfertaDoacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuarioDoador = table.Column<int>(type: "int", nullable: false),
                    IdUsuarioOng = table.Column<int>(type: "int", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsCestaCompleta = table.Column<bool>(type: "bit", nullable: false),
                    QtdeCestas = table.Column<int>(type: "int", nullable: false),
                    TipoTransporte = table.Column<int>(type: "int", nullable: false),
                    DataAgendada = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataConclusao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfertaDoacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfertaDoacao_Usuarios_IdUsuarioDoador",
                        column: x => x.IdUsuarioDoador,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OfertaDoacao_Usuarios_IdUsuarioOng",
                        column: x => x.IdUsuarioOng,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemOferta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOferta = table.Column<int>(type: "int", nullable: false),
                    Alimento = table.Column<int>(type: "int", nullable: false),
                    QtdeItem = table.Column<int>(type: "int", nullable: false),
                    MedidaItem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemOferta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemOferta_OfertaDoacao_IdOferta",
                        column: x => x.IdOferta,
                        principalTable: "OfertaDoacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpcaoAgendamentoOferta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdOferta = table.Column<int>(type: "int", nullable: false),
                    DiaDaSemana = table.Column<int>(type: "int", nullable: false),
                    HorarioInicio = table.Column<TimeSpan>(type: "time", nullable: false),
                    HorarioFinal = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpcaoAgendamentoOferta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpcaoAgendamentoOferta_OfertaDoacao_IdOferta",
                        column: x => x.IdOferta,
                        principalTable: "OfertaDoacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemOferta_IdOferta",
                table: "ItemOferta",
                column: "IdOferta");

            migrationBuilder.CreateIndex(
                name: "IX_OfertaDoacao_IdUsuarioDoador",
                table: "OfertaDoacao",
                column: "IdUsuarioDoador");

            migrationBuilder.CreateIndex(
                name: "IX_OfertaDoacao_IdUsuarioOng",
                table: "OfertaDoacao",
                column: "IdUsuarioOng");

            migrationBuilder.CreateIndex(
                name: "IX_OpcaoAgendamentoOferta_IdOferta",
                table: "OpcaoAgendamentoOferta",
                column: "IdOferta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemOferta");

            migrationBuilder.DropTable(
                name: "OpcaoAgendamentoOferta");

            migrationBuilder.DropTable(
                name: "OfertaDoacao");

            migrationBuilder.AlterColumn<string>(
                name: "Complemento",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
