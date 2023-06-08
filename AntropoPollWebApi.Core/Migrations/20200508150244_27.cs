using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AntropoPollWebApi.Core.Migrations
{
    public partial class _27 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResultInterpretation",
                schema: "antropopoll",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    InterpretationId = table.Column<Guid>(nullable: false),
                    ResultId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultInterpretation", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_ResultInterpretation_Interpretations_InterpretationId",
                        column: x => x.InterpretationId,
                        principalSchema: "antropopoll",
                        principalTable: "Interpretations",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultInterpretation_Results_ResultId",
                        column: x => x.ResultId,
                        principalSchema: "antropopoll",
                        principalTable: "Results",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResultInterpretation_InterpretationId",
                schema: "antropopoll",
                table: "ResultInterpretation",
                column: "InterpretationId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultInterpretation_ResultId",
                schema: "antropopoll",
                table: "ResultInterpretation",
                column: "ResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResultInterpretation",
                schema: "antropopoll");
        }
    }
}
