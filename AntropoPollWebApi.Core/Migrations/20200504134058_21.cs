using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AntropoPollWebApi.Core.Migrations
{
    public partial class _21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Interpretations",
                schema: "antropopoll",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SchemaId = table.Column<Guid>(nullable: false),
                    SchemaVariableId = table.Column<Guid>(nullable: false),
                    ValueMin = table.Column<decimal>(nullable: false),
                    ValueMax = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interpretations", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Interpretations_Schemes_SchemaId",
                        column: x => x.SchemaId,
                        principalSchema: "antropopoll",
                        principalTable: "Schemes",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interpretations_SchemaVariables_SchemaVariableId",
                        column: x => x.SchemaVariableId,
                        principalSchema: "antropopoll",
                        principalTable: "SchemaVariables",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interpretations_SchemaId",
                schema: "antropopoll",
                table: "Interpretations",
                column: "SchemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Interpretations_SchemaVariableId",
                schema: "antropopoll",
                table: "Interpretations",
                column: "SchemaVariableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Interpretations",
                schema: "antropopoll");
        }
    }
}
