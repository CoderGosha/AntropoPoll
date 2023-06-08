using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AntropoPollWebApi.Core.Migrations
{
    public partial class _20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Question_BaseQuestionGuid",
                schema: "antropopoll",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "QuestionGuid",
                schema: "antropopoll",
                table: "Answers");

            migrationBuilder.AlterColumn<Guid>(
                name: "BaseQuestionGuid",
                schema: "antropopoll",
                table: "Answers",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Question_BaseQuestionGuid",
                schema: "antropopoll",
                table: "Answers",
                column: "BaseQuestionGuid",
                principalSchema: "antropopoll",
                principalTable: "Question",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Question_BaseQuestionGuid",
                schema: "antropopoll",
                table: "Answers");

            migrationBuilder.AlterColumn<Guid>(
                name: "BaseQuestionGuid",
                schema: "antropopoll",
                table: "Answers",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "QuestionGuid",
                schema: "antropopoll",
                table: "Answers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Question_BaseQuestionGuid",
                schema: "antropopoll",
                table: "Answers",
                column: "BaseQuestionGuid",
                principalSchema: "antropopoll",
                principalTable: "Question",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
