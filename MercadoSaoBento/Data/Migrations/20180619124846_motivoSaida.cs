using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MercadoSaoBento.Data.Migrations
{
    public partial class motivoSaida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MotivoSaida",
                table: "movSaida",
                newName: "MotivosSaidaID");

            migrationBuilder.CreateTable(
                name: "MotivosSaida",
                columns: table => new
                {
                    MotivosSaidaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    motivo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotivosSaida", x => x.MotivosSaidaID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_movSaida_MotivosSaidaID",
                table: "movSaida",
                column: "MotivosSaidaID");

            migrationBuilder.AddForeignKey(
                name: "FK_movSaida_MotivosSaida_MotivosSaidaID",
                table: "movSaida",
                column: "MotivosSaidaID",
                principalTable: "MotivosSaida",
                principalColumn: "MotivosSaidaID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_movSaida_MotivosSaida_MotivosSaidaID",
                table: "movSaida");

            migrationBuilder.DropTable(
                name: "MotivosSaida");

            migrationBuilder.DropIndex(
                name: "IX_movSaida_MotivosSaidaID",
                table: "movSaida");

            migrationBuilder.RenameColumn(
                name: "MotivosSaidaID",
                table: "movSaida",
                newName: "MotivoSaida");
        }
    }
}
