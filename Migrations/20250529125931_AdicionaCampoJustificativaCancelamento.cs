using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaCampoJustificativaCancelamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JustificativaCancelamento",
                table: "SolicitacaoDoacao",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SolicitacaoDoacaoId",
                table: "ItemSolicitacao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ItemSolicitacao_SolicitacaoDoacaoId",
                table: "ItemSolicitacao",
                column: "SolicitacaoDoacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSolicitacao_SolicitacaoDoacao_SolicitacaoDoacaoId",
                table: "ItemSolicitacao",
                column: "SolicitacaoDoacaoId",
                principalTable: "SolicitacaoDoacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemSolicitacao_SolicitacaoDoacao_SolicitacaoDoacaoId",
                table: "ItemSolicitacao");

            migrationBuilder.DropIndex(
                name: "IX_ItemSolicitacao_SolicitacaoDoacaoId",
                table: "ItemSolicitacao");

            migrationBuilder.DropColumn(
                name: "JustificativaCancelamento",
                table: "SolicitacaoDoacao");

            migrationBuilder.DropColumn(
                name: "SolicitacaoDoacaoId",
                table: "ItemSolicitacao");
        }
    }
}
