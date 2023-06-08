using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AntropoPollWebApi.Core.Migrations
{
    public partial class _22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interpretations_SchemaVariables_SchemaVariableId",
                schema: "antropopoll",
                table: "Interpretations");

            migrationBuilder.DropIndex(
                name: "IX_Interpretations_SchemaVariableId",
                schema: "antropopoll",
                table: "Interpretations");

            migrationBuilder.DropColumn(
                name: "SchemaVariableId",
                schema: "antropopoll",
                table: "Interpretations");

            migrationBuilder.DropColumn(
                name: "ValueMax",
                schema: "antropopoll",
                table: "Interpretations");

            migrationBuilder.DropColumn(
                name: "ValueMin",
                schema: "antropopoll",
                table: "Interpretations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SchemaVariableId",
                schema: "antropopoll",
                table: "Interpretations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "ValueMax",
                schema: "antropopoll",
                table: "Interpretations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ValueMin",
                schema: "antropopoll",
                table: "Interpretations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Interpretations_SchemaVariableId",
                schema: "antropopoll",
                table: "Interpretations",
                column: "SchemaVariableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Interpretations_SchemaVariables_SchemaVariableId",
                schema: "antropopoll",
                table: "Interpretations",
                column: "SchemaVariableId",
                principalSchema: "antropopoll",
                principalTable: "SchemaVariables",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
