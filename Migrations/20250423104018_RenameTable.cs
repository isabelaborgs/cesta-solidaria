using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class RenameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfertaDoacao_Usuarios_IdUsuarioDoador",
                table: "OfertaDoacao");

            migrationBuilder.DropForeignKey(
                name: "FK_OpcoesAgendamentoSolicit_SolicitacoesDoacao_SolicitacaoDoacaoId",
                table: "OpcoesAgendamentoSolicit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SolicitacoesDoacao",
                table: "SolicitacoesDoacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OpcoesAgendamentoSolicit",
                table: "OpcoesAgendamentoSolicit");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuario");

            migrationBuilder.RenameTable(
                name: "SolicitacoesDoacao",
                newName: "SolicitacaoDoacao");

            migrationBuilder.RenameTable(
                name: "OpcoesAgendamentoSolicit",
                newName: "OpcaoAgendamentoSolicit");

            migrationBuilder.RenameIndex(
                name: "IX_OpcoesAgendamentoSolicit_SolicitacaoDoacaoId",
                table: "OpcaoAgendamentoSolicit",
                newName: "IX_OpcaoAgendamentoSolicit_SolicitacaoDoacaoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SolicitacaoDoacao",
                table: "SolicitacaoDoacao",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpcaoAgendamentoSolicit",
                table: "OpcaoAgendamentoSolicit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfertaDoacao_Usuario_IdUsuarioDoador",
                table: "OfertaDoacao",
                column: "IdUsuarioDoador",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OpcaoAgendamentoSolicit_SolicitacaoDoacao_SolicitacaoDoacaoId",
                table: "OpcaoAgendamentoSolicit",
                column: "SolicitacaoDoacaoId",
                principalTable: "SolicitacaoDoacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfertaDoacao_Usuario_IdUsuarioDoador",
                table: "OfertaDoacao");

            migrationBuilder.DropForeignKey(
                name: "FK_OpcaoAgendamentoSolicit_SolicitacaoDoacao_SolicitacaoDoacaoId",
                table: "OpcaoAgendamentoSolicit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SolicitacaoDoacao",
                table: "SolicitacaoDoacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OpcaoAgendamentoSolicit",
                table: "OpcaoAgendamentoSolicit");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "SolicitacaoDoacao",
                newName: "SolicitacoesDoacao");

            migrationBuilder.RenameTable(
                name: "OpcaoAgendamentoSolicit",
                newName: "OpcoesAgendamentoSolicit");

            migrationBuilder.RenameIndex(
                name: "IX_OpcaoAgendamentoSolicit_SolicitacaoDoacaoId",
                table: "OpcoesAgendamentoSolicit",
                newName: "IX_OpcoesAgendamentoSolicit_SolicitacaoDoacaoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SolicitacoesDoacao",
                table: "SolicitacoesDoacao",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpcoesAgendamentoSolicit",
                table: "OpcoesAgendamentoSolicit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfertaDoacao_Usuarios_IdUsuarioDoador",
                table: "OfertaDoacao",
                column: "IdUsuarioDoador",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OpcoesAgendamentoSolicit_SolicitacoesDoacao_SolicitacaoDoacaoId",
                table: "OpcoesAgendamentoSolicit",
                column: "SolicitacaoDoacaoId",
                principalTable: "SolicitacoesDoacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
