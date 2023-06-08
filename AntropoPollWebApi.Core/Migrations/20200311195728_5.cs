using Microsoft.EntityFrameworkCore.Migrations;
using System.Text.Json;

namespace AntropoPollWebApi.Core.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<JsonDocument>(
                name: "FormAnalytics",
                schema: "antropopoll",
                table: "Results",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormAnalytics",
                schema: "antropopoll",
                table: "Results");
        }
    }
}
