using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApplicationDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItensSolicitacao_SolicitacaoDoacao_SolicitacaoDoacaoId",
                table: "ItensSolicitacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItensSolicitacao",
                table: "ItensSolicitacao");

            migrationBuilder.RenameTable(
                name: "ItensSolicitacao",
                newName: "ItemSolicitacao");

            migrationBuilder.RenameIndex(
                name: "IX_ItensSolicitacao_SolicitacaoDoacaoId",
                table: "ItemSolicitacao",
                newName: "IX_ItemSolicitacao_SolicitacaoDoacaoId");

            migrationBuilder.AlterColumn<int>(
                name: "Estado",
                table: "Usuario",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "SolicitacaoDoacao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemSolicitacao",
                table: "ItemSolicitacao",
                column: "Id");

            //migrationBuilder.CreateTable(
            //    name: "Notificacao",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        DataNotificacao = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        TipoNotificacao = table.Column<int>(type: "int", nullable: false),
            //        IsLida = table.Column<bool>(type: "bit", nullable: false),
            //        IdUsuario = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Notificacao", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Notificacao_Usuario_IdUsuario",
            //            column: x => x.IdUsuario,
            //            principalTable: "Usuario",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Notificacao_IdUsuario",
            //    table: "Notificacao",
            //    column: "IdUsuario");

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

            migrationBuilder.DropTable(
                name: "Notificacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemSolicitacao",
                table: "ItemSolicitacao");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SolicitacaoDoacao");

            migrationBuilder.RenameTable(
                name: "ItemSolicitacao",
                newName: "ItensSolicitacao");

            migrationBuilder.RenameIndex(
                name: "IX_ItemSolicitacao_SolicitacaoDoacaoId",
                table: "ItensSolicitacao",
                newName: "IX_ItensSolicitacao_SolicitacaoDoacaoId");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Usuario",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

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
    }
}
