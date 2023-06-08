using Microsoft.EntityFrameworkCore.Migrations;

namespace AntropoPollWebApi.Core.Migrations
{
    public partial class _33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "antropopoll",
                table: "Schemes",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "antropopoll",
                table: "Schemes");
        }
    }
}
