using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AntropoPollWebApi.Core.Migrations
{
    public partial class _13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Clients_ClientId",
                schema: "antropopoll",
                table: "Results");

            migrationBuilder.DropTable(
                name: "Clients",
                schema: "antropopoll");

            migrationBuilder.DropIndex(
                name: "IX_Results_ClientId",
                schema: "antropopoll",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "ClientId",
                schema: "antropopoll",
                table: "Results");

            migrationBuilder.AddColumn<Guid>(
                name: "InviteId",
                schema: "antropopoll",
                table: "Results",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Invites",
                schema: "antropopoll",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    SchemaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invites", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Invites_Schemes_SchemaId",
                        column: x => x.SchemaId,
                        principalSchema: "antropopoll",
                        principalTable: "Schemes",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Results_InviteId",
                schema: "antropopoll",
                table: "Results",
                column: "InviteId");

            migrationBuilder.CreateIndex(
                name: "IX_Invites_SchemaId",
                schema: "antropopoll",
                table: "Invites",
                column: "SchemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Invites_InviteId",
                schema: "antropopoll",
                table: "Results",
                column: "InviteId",
                principalSchema: "antropopoll",
                principalTable: "Invites",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Invites_InviteId",
                schema: "antropopoll",
                table: "Results");

            migrationBuilder.DropTable(
                name: "Invites",
                schema: "antropopoll");

            migrationBuilder.DropIndex(
                name: "IX_Results_InviteId",
                schema: "antropopoll",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "InviteId",
                schema: "antropopoll",
                table: "Results");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                schema: "antropopoll",
                table: "Results",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "antropopoll",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    SchemaId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Clients_Schemes_SchemaId",
                        column: x => x.SchemaId,
                        principalSchema: "antropopoll",
                        principalTable: "Schemes",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Results_ClientId",
                schema: "antropopoll",
                table: "Results",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_SchemaId",
                schema: "antropopoll",
                table: "Clients",
                column: "SchemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Clients_ClientId",
                schema: "antropopoll",
                table: "Results",
                column: "ClientId",
                principalSchema: "antropopoll",
                principalTable: "Clients",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
