using Microsoft.EntityFrameworkCore.Migrations;

namespace AntropoPollWebApi.Core.Migrations
{
    public partial class _31 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResultInterpretation_Interpretations_InterpretationId",
                schema: "antropopoll",
                table: "ResultInterpretation");

            migrationBuilder.DropForeignKey(
                name: "FK_ResultInterpretation_Results_ResultId",
                schema: "antropopoll",
                table: "ResultInterpretation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResultInterpretation",
                schema: "antropopoll",
                table: "ResultInterpretation");

            migrationBuilder.RenameTable(
                name: "ResultInterpretation",
                schema: "antropopoll",
                newName: "ResultInterpretations",
                newSchema: "antropopoll");

            migrationBuilder.RenameIndex(
                name: "IX_ResultInterpretation_ResultId",
                schema: "antropopoll",
                table: "ResultInterpretations",
                newName: "IX_ResultInterpretations_ResultId");

            migrationBuilder.RenameIndex(
                name: "IX_ResultInterpretation_InterpretationId",
                schema: "antropopoll",
                table: "ResultInterpretations",
                newName: "IX_ResultInterpretations_InterpretationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResultInterpretations",
                schema: "antropopoll",
                table: "ResultInterpretations",
                column: "Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_ResultInterpretations_Interpretations_InterpretationId",
                schema: "antropopoll",
                table: "ResultInterpretations",
                column: "InterpretationId",
                principalSchema: "antropopoll",
                principalTable: "Interpretations",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResultInterpretations_Results_ResultId",
                schema: "antropopoll",
                table: "ResultInterpretations",
                column: "ResultId",
                principalSchema: "antropopoll",
                principalTable: "Results",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResultInterpretations_Interpretations_InterpretationId",
                schema: "antropopoll",
                table: "ResultInterpretations");

            migrationBuilder.DropForeignKey(
                name: "FK_ResultInterpretations_Results_ResultId",
                schema: "antropopoll",
                table: "ResultInterpretations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResultInterpretations",
                schema: "antropopoll",
                table: "ResultInterpretations");

            migrationBuilder.RenameTable(
                name: "ResultInterpretations",
                schema: "antropopoll",
                newName: "ResultInterpretation",
                newSchema: "antropopoll");

            migrationBuilder.RenameIndex(
                name: "IX_ResultInterpretations_ResultId",
                schema: "antropopoll",
                table: "ResultInterpretation",
                newName: "IX_ResultInterpretation_ResultId");

            migrationBuilder.RenameIndex(
                name: "IX_ResultInterpretations_InterpretationId",
                schema: "antropopoll",
                table: "ResultInterpretation",
                newName: "IX_ResultInterpretation_InterpretationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResultInterpretation",
                schema: "antropopoll",
                table: "ResultInterpretation",
                column: "Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_ResultInterpretation_Interpretations_InterpretationId",
                schema: "antropopoll",
                table: "ResultInterpretation",
                column: "InterpretationId",
                principalSchema: "antropopoll",
                principalTable: "Interpretations",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResultInterpretation_Results_ResultId",
                schema: "antropopoll",
                table: "ResultInterpretation",
                column: "ResultId",
                principalSchema: "antropopoll",
                principalTable: "Results",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
