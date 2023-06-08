﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AntropoPollWebApi.Core.Migrations
{
    public partial class _14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "antropopoll",
                table: "Invites",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Invites_UserId",
                schema: "antropopoll",
                table: "Invites",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invites_Users_UserId",
                schema: "antropopoll",
                table: "Invites",
                column: "UserId",
                principalSchema: "antropopoll",
                principalTable: "Users",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invites_Users_UserId",
                schema: "antropopoll",
                table: "Invites");

            migrationBuilder.DropIndex(
                name: "IX_Invites_UserId",
                schema: "antropopoll",
                table: "Invites");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "antropopoll",
                table: "Invites");
        }
    }
}
