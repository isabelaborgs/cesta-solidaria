using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class AddIdOngSolicitacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacao_OfertaDoacao_IdOferta",
                table: "Transacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacao_SolicitacaoDoacao_IdSolicitacao",
                table: "Transacao");

            migrationBuilder.AlterColumn<int>(
                name: "IdSolicitacao",
                table: "Transacao",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdOferta",
                table: "Transacao",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataConclusao",
                table: "Transacao",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "IdUsuarioOng",
                table: "SolicitacaoDoacao",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoDoacao_IdUsuarioOng",
                table: "SolicitacaoDoacao",
                column: "IdUsuarioOng");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitacaoDoacao_Usuario_IdUsuarioOng",
                table: "SolicitacaoDoacao",
                column: "IdUsuarioOng",
                principalTable: "Usuario",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacao_OfertaDoacao_IdOferta",
                table: "Transacao",
                column: "IdOferta",
                principalTable: "OfertaDoacao",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacao_SolicitacaoDoacao_IdSolicitacao",
                table: "Transacao",
                column: "IdSolicitacao",
                principalTable: "SolicitacaoDoacao",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitacaoDoacao_Usuario_IdUsuarioOng",
                table: "SolicitacaoDoacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacao_OfertaDoacao_IdOferta",
                table: "Transacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacao_SolicitacaoDoacao_IdSolicitacao",
                table: "Transacao");

            migrationBuilder.DropIndex(
                name: "IX_SolicitacaoDoacao_IdUsuarioOng",
                table: "SolicitacaoDoacao");

            migrationBuilder.DropColumn(
                name: "IdUsuarioOng",
                table: "SolicitacaoDoacao");

            migrationBuilder.AlterColumn<int>(
                name: "IdSolicitacao",
                table: "Transacao",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdOferta",
                table: "Transacao",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataConclusao",
                table: "Transacao",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transacao_OfertaDoacao_IdOferta",
                table: "Transacao",
                column: "IdOferta",
                principalTable: "OfertaDoacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transacao_SolicitacaoDoacao_IdSolicitacao",
                table: "Transacao",
                column: "IdSolicitacao",
                principalTable: "SolicitacaoDoacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
