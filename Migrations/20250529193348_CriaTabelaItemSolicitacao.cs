using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class CriaTabelaItemSolicitacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemSolicitacao_SolicitacaoDoacao_SolicitacaoDoacaoId",
                table: "ItemSolicitacao");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "ItemSolicitacao");

            migrationBuilder.RenameTable(
                name: "ItemSolicitacao",
                newName: "ItensSolicitacao");

            migrationBuilder.RenameIndex(
                name: "IX_ItemSolicitacao_SolicitacaoDoacaoId",
                table: "ItensSolicitacao",
                newName: "IX_ItensSolicitacao_SolicitacaoDoacaoId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ItensSolicitacao",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItensSolicitacao",
                table: "ItensSolicitacao",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensSolicitacao_SolicitacaoDoacao_SolicitacaoDoacaoId",
                table: "ItensSolicitacao",
                column: "SolicitacaoDoacaoId",
                principalTable: "SolicitacaoDoacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensSolicitacao_SolicitacaoDoacao_SolicitacaoDoacaoId",
                table: "ItensSolicitacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItensSolicitacao",
                table: "ItensSolicitacao");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ItensSolicitacao");

            migrationBuilder.RenameTable(
                name: "ItensSolicitacao",
                newName: "ItemSolicitacao");

            migrationBuilder.RenameIndex(
                name: "IX_ItensSolicitacao_SolicitacaoDoacaoId",
                table: "ItemSolicitacao",
                newName: "IX_ItemSolicitacao_SolicitacaoDoacaoId");

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "ItemSolicitacao",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSolicitacao_SolicitacaoDoacao_SolicitacaoDoacaoId",
                table: "ItemSolicitacao",
                column: "SolicitacaoDoacaoId",
                principalTable: "SolicitacaoDoacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
