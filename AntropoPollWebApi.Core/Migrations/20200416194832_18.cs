using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AntropoPollWebApi.Core.Migrations
{
    public partial class _18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invites_Schemes_SchemaId",
                schema: "antropopoll",
                table: "Invites");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Schemes_SchemaId",
                schema: "antropopoll",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_SchemaId",
                schema: "antropopoll",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Invites_SchemaId",
                schema: "antropopoll",
                table: "Invites");

            migrationBuilder.DropColumn(
                name: "SchemaId",
                schema: "antropopoll",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "SchemaId",
                schema: "antropopoll",
                table: "Invites");

            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                schema: "antropopoll",
                table: "Results",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                schema: "antropopoll",
                table: "Invites",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Results_EventId",
                schema: "antropopoll",
                table: "Results",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Invites_EventId",
                schema: "antropopoll",
                table: "Invites",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invites_Events_EventId",
                schema: "antropopoll",
                table: "Invites",
                column: "EventId",
                principalSchema: "antropopoll",
                principalTable: "Events",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Events_EventId",
                schema: "antropopoll",
                table: "Results",
                column: "EventId",
                principalSchema: "antropopoll",
                principalTable: "Events",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invites_Events_EventId",
                schema: "antropopoll",
                table: "Invites");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Events_EventId",
                schema: "antropopoll",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_EventId",
                schema: "antropopoll",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Invites_EventId",
                schema: "antropopoll",
                table: "Invites");

            migrationBuilder.DropColumn(
                name: "EventId",
                schema: "antropopoll",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "EventId",
                schema: "antropopoll",
                table: "Invites");

            migrationBuilder.AddColumn<Guid>(
                name: "SchemaId",
                schema: "antropopoll",
                table: "Results",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SchemaId",
                schema: "antropopoll",
                table: "Invites",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Results_SchemaId",
                schema: "antropopoll",
                table: "Results",
                column: "SchemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Invites_SchemaId",
                schema: "antropopoll",
                table: "Invites",
                column: "SchemaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invites_Schemes_SchemaId",
                schema: "antropopoll",
                table: "Invites",
                column: "SchemaId",
                principalSchema: "antropopoll",
                principalTable: "Schemes",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

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
    }
}
