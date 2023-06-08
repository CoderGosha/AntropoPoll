using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AntropoPollWebApi.Core.Migrations
{
    public partial class _17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                schema: "antropopoll",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SchemaId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    DateTimeBegin = table.Column<DateTime>(nullable: true),
                    DateTimeEnd = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    EventActiveType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Events_Schemes_SchemaId",
                        column: x => x.SchemaId,
                        principalSchema: "antropopoll",
                        principalTable: "Schemes",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Events_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "antropopoll",
                        principalTable: "Users",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_SchemaId",
                schema: "antropopoll",
                table: "Events",
                column: "SchemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_UserId",
                schema: "antropopoll",
                table: "Events",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events",
                schema: "antropopoll");
        }
    }
}
