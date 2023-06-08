using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AntropoPollWebApi.Core.Migrations
{
    public partial class _15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemVariableReports",
                schema: "antropopoll",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    InviteId = table.Column<Guid>(nullable: false),
                    SchemaVariableId = table.Column<Guid>(nullable: false),
                    Value = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemVariableReports", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_SystemVariableReports_Invites_InviteId",
                        column: x => x.InviteId,
                        principalSchema: "antropopoll",
                        principalTable: "Invites",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SystemVariableReports_SchemaVariables_SchemaVariableId",
                        column: x => x.SchemaVariableId,
                        principalSchema: "antropopoll",
                        principalTable: "SchemaVariables",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemVariableReports_InviteId",
                schema: "antropopoll",
                table: "SystemVariableReports",
                column: "InviteId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemVariableReports_SchemaVariableId",
                schema: "antropopoll",
                table: "SystemVariableReports",
                column: "SchemaVariableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemVariableReports",
                schema: "antropopoll");
        }
    }
}
