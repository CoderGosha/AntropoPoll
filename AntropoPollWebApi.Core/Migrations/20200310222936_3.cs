using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AntropoPollWebApi.Core.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Results",
                schema: "antropopoll",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "ResultQuestions",
                schema: "antropopoll",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    BaseQuestionId = table.Column<Guid>(nullable: false),
                    AnswerId = table.Column<Guid>(nullable: false),
                    ResultId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultQuestions", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_ResultQuestions_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalSchema: "antropopoll",
                        principalTable: "Answers",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultQuestions_Question_BaseQuestionId",
                        column: x => x.BaseQuestionId,
                        principalSchema: "antropopoll",
                        principalTable: "Question",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultQuestions_Results_ResultId",
                        column: x => x.ResultId,
                        principalSchema: "antropopoll",
                        principalTable: "Results",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResultQuestions_AnswerId",
                schema: "antropopoll",
                table: "ResultQuestions",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultQuestions_BaseQuestionId",
                schema: "antropopoll",
                table: "ResultQuestions",
                column: "BaseQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultQuestions_ResultId",
                schema: "antropopoll",
                table: "ResultQuestions",
                column: "ResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResultQuestions",
                schema: "antropopoll");

            migrationBuilder.DropTable(
                name: "Results",
                schema: "antropopoll");
        }
    }
}
