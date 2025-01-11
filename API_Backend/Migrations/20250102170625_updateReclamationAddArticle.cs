using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Backend.Migrations
{
    /// <inheritdoc />
    public partial class updateReclamationAddArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Etat_Reclamation",
                table: "Reclamations",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "En_attente_information",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "En_attente_information");

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "Reclamations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reclamations_ArticleId",
                table: "Reclamations",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reclamations_Articles_ArticleId",
                table: "Reclamations",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reclamations_Articles_ArticleId",
                table: "Reclamations");

            migrationBuilder.DropIndex(
                name: "IX_Reclamations_ArticleId",
                table: "Reclamations");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Reclamations");

            migrationBuilder.AlterColumn<string>(
                name: "Etat_Reclamation",
                table: "Reclamations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "En_attente_information",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "En_attente_information");
        }
    }
}
