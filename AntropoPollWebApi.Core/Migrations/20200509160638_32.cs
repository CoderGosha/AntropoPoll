using Microsoft.EntityFrameworkCore.Migrations;

namespace AntropoPollWebApi.Core.Migrations
{
    public partial class _32 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TemplateData",
                schema: "antropopoll",
                table: "Results",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TemplateData",
                schema: "antropopoll",
                table: "Results");
        }
    }
}
