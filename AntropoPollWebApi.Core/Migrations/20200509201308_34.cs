using Microsoft.EntityFrameworkCore.Migrations;

namespace AntropoPollWebApi.Core.Migrations
{
    public partial class _34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_SchemaVariables_SchemaVariableId",
                schema: "antropopoll",
                table: "Question");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_SchemaVariables_SchemaVariableId",
                schema: "antropopoll",
                table: "Question",
                column: "SchemaVariableId",
                principalSchema: "antropopoll",
                principalTable: "SchemaVariables",
                principalColumn: "Guid",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Question_SchemaVariables_SchemaVariableId",
                schema: "antropopoll",
                table: "Question");

            migrationBuilder.AddForeignKey(
                name: "FK_Question_SchemaVariables_SchemaVariableId",
                schema: "antropopoll",
                table: "Question",
                column: "SchemaVariableId",
                principalSchema: "antropopoll",
                principalTable: "SchemaVariables",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
