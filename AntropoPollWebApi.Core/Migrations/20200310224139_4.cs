using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AntropoPollWebApi.Core.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SchemaId",
                schema: "antropopoll",
                table: "Results",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Results_SchemaId",
                schema: "antropopoll",
                table: "Results",
                column: "SchemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Schemes_SchemaId",
                schema: "antropopoll",
                table: "Results",
                column: "SchemaId",
                principalSchema: "antropopoll",
                principalTable: "Schemes",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Schemes_SchemaId",
                schema: "antropopoll",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_SchemaId",
                schema: "antropopoll",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "SchemaId",
                schema: "antropopoll",
                table: "Results");
        }
    }
}
