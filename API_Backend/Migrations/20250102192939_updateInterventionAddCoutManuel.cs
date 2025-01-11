using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Backend.Migrations
{
    /// <inheritdoc />
    public partial class updateInterventionAddCoutManuel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "EstGratuite",
                table: "Interventions",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<double>(
                name: "CoutManuel",
                table: "Interventions",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoutManuel",
                table: "Interventions");

            migrationBuilder.AlterColumn<bool>(
                name: "EstGratuite",
                table: "Interventions",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
