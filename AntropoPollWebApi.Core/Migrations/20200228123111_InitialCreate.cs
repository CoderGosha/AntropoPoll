using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AntropoPollWebApi.Core.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "antropopoll");

            migrationBuilder.CreateTable(
                name: "Question",
                schema: "antropopoll",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    QuestionType = table.Column<int>(nullable: false),
                    QuestionDiscriminator = table.Column<int>(nullable: false),
                    MaxCountСhoice = table.Column<int>(nullable: true),
                    MinCountСhoice = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                schema: "antropopoll",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    QuestionGuid = table.Column<Guid>(nullable: false),
                    BaseQuestionGuid = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Answers_Question_BaseQuestionGuid",
                        column: x => x.BaseQuestionGuid,
                        principalSchema: "antropopoll",
                        principalTable: "Question",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_BaseQuestionGuid",
                schema: "antropopoll",
                table: "Answers",
                column: "BaseQuestionGuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers",
                schema: "antropopoll");

            migrationBuilder.DropTable(
                name: "Question",
                schema: "antropopoll");
        }
    }
}
