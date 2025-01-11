using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddPieceUtilise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PieceRechanges_Interventions_InterventionId",
                table: "PieceRechanges");

            migrationBuilder.DropIndex(
                name: "IX_PieceRechanges_InterventionId",
                table: "PieceRechanges");

            migrationBuilder.DropColumn(
                name: "InterventionId",
                table: "PieceRechanges");

            migrationBuilder.AlterColumn<string>(
                name: "Statut",
                table: "Interventions",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "En_attente",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "En_attente");

            migrationBuilder.AlterColumn<double>(
                name: "MontantTotal",
                table: "Factures",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFacture",
                table: "Factures",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "PieceUtilises",
                columns: table => new
                {
                    PieceUtiliseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterventionId = table.Column<int>(type: "int", nullable: false),
                    PieceRechangeId = table.Column<int>(type: "int", nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PieceUtilises", x => x.PieceUtiliseId);
                    table.ForeignKey(
                        name: "FK_PieceUtilises_Interventions_InterventionId",
                        column: x => x.InterventionId,
                        principalTable: "Interventions",
                        principalColumn: "InterventionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PieceUtilises_PieceRechanges_PieceRechangeId",
                        column: x => x.PieceRechangeId,
                        principalTable: "PieceRechanges",
                        principalColumn: "PieceRechangeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PieceUtilises_InterventionId",
                table: "PieceUtilises",
                column: "InterventionId");

            migrationBuilder.CreateIndex(
                name: "IX_PieceUtilises_PieceRechangeId",
                table: "PieceUtilises",
                column: "PieceRechangeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PieceUtilises");

            migrationBuilder.AddColumn<int>(
                name: "InterventionId",
                table: "PieceRechanges",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Statut",
                table: "Interventions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "En_attente",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "En_attente");

            migrationBuilder.AlterColumn<double>(
                name: "MontantTotal",
                table: "Factures",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFacture",
                table: "Factures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PieceRechanges_InterventionId",
                table: "PieceRechanges",
                column: "InterventionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PieceRechanges_Interventions_InterventionId",
                table: "PieceRechanges",
                column: "InterventionId",
                principalTable: "Interventions",
                principalColumn: "InterventionId");
        }
    }
}
