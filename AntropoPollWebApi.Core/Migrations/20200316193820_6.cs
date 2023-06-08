using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AntropoPollWebApi.Core.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SchemaVariableId",
                schema: "antropopoll",
                table: "Question",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "VariableValue",
                schema: "antropopoll",
                table: "Answers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SchemaVariables",
                schema: "antropopoll",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SchemaId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchemaVariables", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_SchemaVariables_Schemes_SchemaId",
                        column: x => x.SchemaId,
                        principalSchema: "antropopoll",
                        principalTable: "Schemes",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Question_SchemaVariableId",
                schema: "antropopoll",
                table: "Question",
                column: "SchemaVariableId");

            migrationBuilder.CreateIndex(
                name: "IX_SchemaVariables_SchemaId",
                schema: "antropopoll",
                table: "SchemaVariables",
                column: "SchemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_SchemaVariables_SchemaVariableId",
                schema: "antropopoll",
                table: "Question",
                column: "SchemaVariableId",
                principalSchema: "antropopoll",
                principalTable: "SchemaVariables",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_SchemaVariables_SchemaVariableId",
                schema: "antropopoll",
                table: "Question");

            migrationBuilder.DropTable(
                name: "SchemaVariables",
                schema: "antropopoll");

            migrationBuilder.DropIndex(
                name: "IX_Question_SchemaVariableId",
                schema: "antropopoll",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "SchemaVariableId",
                schema: "antropopoll",
                table: "Question");

            migrationBuilder.DropColumn(
                name: "VariableValue",
                schema: "antropopoll",
                table: "Answers");
        }
    }
}
