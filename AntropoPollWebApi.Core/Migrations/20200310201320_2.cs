using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AntropoPollWebApi.Core.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SchemaId",
                schema: "antropopoll",
                table: "Question",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Schemes",
                schema: "antropopoll",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schemes", x => x.Guid);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Question_SchemaId",
                schema: "antropopoll",
                table: "Question",
                column: "SchemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_Schemes_SchemaId",
                schema: "antropopoll",
                table: "Question",
                column: "SchemaId",
                principalSchema: "antropopoll",
                principalTable: "Schemes",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_Schemes_SchemaId",
                schema: "antropopoll",
                table: "Question");

            migrationBuilder.DropTable(
                name: "Schemes",
                schema: "antropopoll");

            migrationBuilder.DropIndex(
                name: "IX_Question_SchemaId",
                schema: "antropopoll",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "SchemaId",
                schema: "antropopoll",
                table: "Question");
        }
    }
}
