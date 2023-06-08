using Microsoft.EntityFrameworkCore.Migrations;

namespace AntropoPollWebApi.Core.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Instruction",
                schema: "antropopoll",
                table: "Question",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Instruction",
                schema: "antropopoll",
                table: "Question");
        }
    }
}
