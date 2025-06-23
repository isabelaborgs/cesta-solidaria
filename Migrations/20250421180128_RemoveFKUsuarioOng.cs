using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFKUsuarioOng : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfertaDoacao_Usuarios_IdUsuarioDoador",
                table: "OfertaDoacao");

            migrationBuilder.DropForeignKey(
                name: "FK_OfertaDoacao_Usuarios_IdUsuarioOng",
                table: "OfertaDoacao");

            migrationBuilder.DropIndex(
                name: "IX_OfertaDoacao_IdUsuarioOng",
                table: "OfertaDoacao");

            migrationBuilder.DropColumn(
                name: "IdUsuarioOng",
                table: "OfertaDoacao");

            migrationBuilder.AddForeignKey(
                name: "FK_OfertaDoacao_Usuarios_IdUsuarioDoador",
                table: "OfertaDoacao",
                column: "IdUsuarioDoador",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfertaDoacao_Usuarios_IdUsuarioDoador",
                table: "OfertaDoacao");

            migrationBuilder.AddColumn<int>(
                name: "IdUsuarioOng",
                table: "OfertaDoacao",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OfertaDoacao_IdUsuarioOng",
                table: "OfertaDoacao",
                column: "IdUsuarioOng");

            migrationBuilder.AddForeignKey(
                name: "FK_OfertaDoacao_Usuarios_IdUsuarioDoador",
                table: "OfertaDoacao",
                column: "IdUsuarioDoador",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OfertaDoacao_Usuarios_IdUsuarioOng",
                table: "OfertaDoacao",
                column: "IdUsuarioOng",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
