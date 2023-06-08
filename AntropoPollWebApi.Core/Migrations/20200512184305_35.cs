using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AntropoPollWebApi.Core.Migrations
{
    public partial class _35 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TemplateData",
                schema: "antropopoll",
                table: "Results");

            migrationBuilder.CreateTable(
                name: "ResultTemplates",
                schema: "antropopoll",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    ResultId = table.Column<Guid>(nullable: false),
                    ReportTemplateId = table.Column<Guid>(nullable: false),
                    TemplateData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultTemplates", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_ResultTemplates_ReportTemplates_ReportTemplateId",
                        column: x => x.ReportTemplateId,
                        principalSchema: "antropopoll",
                        principalTable: "ReportTemplates",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultTemplates_Results_ResultId",
                        column: x => x.ResultId,
                        principalSchema: "antropopoll",
                        principalTable: "Results",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResultTemplates_ReportTemplateId",
                schema: "antropopoll",
                table: "ResultTemplates",
                column: "ReportTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultTemplates_ResultId",
                schema: "antropopoll",
                table: "ResultTemplates",
                column: "ResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResultTemplates",
                schema: "antropopoll");

            migrationBuilder.AddColumn<string>(
                name: "TemplateData",
                schema: "antropopoll",
                table: "Results",
                type: "text",
                nullable: true);
        }
    }
}
