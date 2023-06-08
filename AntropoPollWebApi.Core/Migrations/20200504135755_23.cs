using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AntropoPollWebApi.Core.Migrations
{
    public partial class _23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VariableInInterpretations",
                schema: "antropopoll",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    InterpretationId = table.Column<Guid>(nullable: false),
                    SchemaVariableId = table.Column<Guid>(nullable: false),
                    ValueMin = table.Column<decimal>(nullable: false),
                    ValueMax = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariableInInterpretations", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_VariableInInterpretations_Interpretations_InterpretationId",
                        column: x => x.InterpretationId,
                        principalSchema: "antropopoll",
                        principalTable: "Interpretations",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VariableInInterpretations_SchemaVariables_SchemaVariableId",
                        column: x => x.SchemaVariableId,
                        principalSchema: "antropopoll",
                        principalTable: "SchemaVariables",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VariableInInterpretations_InterpretationId",
                schema: "antropopoll",
                table: "VariableInInterpretations",
                column: "InterpretationId");

            migrationBuilder.CreateIndex(
                name: "IX_VariableInInterpretations_SchemaVariableId",
                schema: "antropopoll",
                table: "VariableInInterpretations",
                column: "SchemaVariableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VariableInInterpretations",
                schema: "antropopoll");
        }
    }
}
