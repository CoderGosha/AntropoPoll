using Microsoft.EntityFrameworkCore.Migrations;

namespace AntropoPollWebApi.Core.Migrations
{
    public partial class _16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxCountСhoice",
                schema: "antropopoll",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "MinCountСhoice",
                schema: "antropopoll",
                table: "Question");

            migrationBuilder.AddColumn<int>(
                name: "MaxCountChoice",
                schema: "antropopoll",
                table: "Question",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinCountChoice",
                schema: "antropopoll",
                table: "Question",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxCountChoice",
                schema: "antropopoll",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "MinCountChoice",
                schema: "antropopoll",
                table: "Question");

            migrationBuilder.AddColumn<int>(
                name: "MaxCountСhoice",
                schema: "antropopoll",
                table: "Question",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinCountСhoice",
                schema: "antropopoll",
                table: "Question",
                type: "integer",
                nullable: true);
        }
    }
}
