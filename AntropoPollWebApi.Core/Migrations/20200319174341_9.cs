using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AntropoPollWebApi.Core.Migrations
{
    public partial class _9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "antropopoll",
                table: "Users",
                columns: new[] { "Guid", "IsSuperUser", "IsModerator", "LastUpdate" },
                values: new object[,]
                {
                    { Guid.NewGuid(), true, true, DateTime.UtcNow },

                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
