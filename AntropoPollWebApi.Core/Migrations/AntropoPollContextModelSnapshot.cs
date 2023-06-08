﻿// <auto-generated />
using System;
using System.Text.Json;
using AntropoPollWebApi.Core.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AntropoPollWebApi.Core.Migrations
{
    [DbContext(typeof(AntropoPollContext))]
    partial class AntropoPollContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("antropopoll")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.Answer", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BaseQuestionGuid")
                        .HasColumnType("uuid");

                    b.Property<int>("Index")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.Property<decimal?>("VariableValue")
                        .HasColumnType("numeric");

                    b.HasKey("Guid");

                    b.HasIndex("BaseQuestionGuid");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.BaseQuestion", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Index")
                        .HasColumnType("integer");

                    b.Property<string>("Instruction")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("QuestionDiscriminator")
                        .HasColumnType("integer");

                    b.Property<int>("QuestionType")
                        .HasColumnType("integer");

                    b.Property<Guid?>("SchemaId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SchemaVariableId")
                        .HasColumnType("uuid");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("Guid");

                    b.HasIndex("SchemaId");

                    b.HasIndex("SchemaVariableId");

                    b.ToTable("Question");

                    b.HasDiscriminator<int>("QuestionDiscriminator");
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.Event", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DateTimeBegin")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("DateTimeEnd")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("EventActiveType")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("SchemaId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Guid");

                    b.HasIndex("SchemaId");

                    b.HasIndex("UserId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.Interpretation", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("SchemaId")
                        .HasColumnType("uuid");

                    b.Property<string>("Tag")
                        .HasColumnType("text");

                    b.HasKey("Guid");

                    b.HasIndex("SchemaId");

                    b.ToTable("Interpretations");
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.Invite", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Guid");

                    b.HasIndex("EventId");

                    b.ToTable("Invites");
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.ReportTemplate", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("SchemaId")
                        .HasColumnType("uuid");

                    b.Property<string>("Template")
                        .HasColumnType("text");

                    b.HasKey("Guid");

                    b.HasIndex("SchemaId");

                    b.ToTable("ReportTemplates");
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.Result", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid");

                    b.Property<JsonDocument>("FormAnalytics")
                        .HasColumnType("jsonb");

                    b.Property<Guid>("InviteId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Guid");

                    b.HasIndex("EventId");

                    b.HasIndex("InviteId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.ResultInterpretation", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("InterpretationId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ResultId")
                        .HasColumnType("uuid");

                    b.HasKey("Guid");

                    b.HasIndex("InterpretationId");

                    b.HasIndex("ResultId");

                    b.ToTable("ResultInterpretations");
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.ResultQuestion", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AnswerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("BaseQuestionId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ResultId")
                        .HasColumnType("uuid");

                    b.HasKey("Guid");

                    b.HasIndex("AnswerId");

                    b.HasIndex("BaseQuestionId");

                    b.HasIndex("ResultId");

                    b.ToTable("ResultQuestions");
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.ResultTemplate", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("ReportTemplateId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ResultId")
                        .HasColumnType("uuid");

                    b.Property<string>("TemplateData")
                        .HasColumnType("text");

                    b.HasKey("Guid");

                    b.HasIndex("ReportTemplateId");

                    b.HasIndex("ResultId");

                    b.ToTable("ResultTemplates");
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.Schema", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("StanValue")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(10);

                    b.HasKey("Guid");

                    b.ToTable("Schemes");
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.SchemaVariable", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid?>("SchemaId")
                        .HasColumnType("uuid");

                    b.HasKey("Guid");

                    b.HasIndex("SchemaId");

                    b.ToTable("SchemaVariables");
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.SystemVariableReport", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("InviteId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("MaxValue")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric")
                        .HasDefaultValue(0m);

                    b.Property<Guid>("SchemaVariableId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("StanValue")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("numeric")
                        .HasDefaultValue(0m);

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasKey("Guid");

                    b.HasIndex("InviteId");

                    b.HasIndex("SchemaVariableId");

                    b.ToTable("SystemVariableReports");
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.User", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<bool>("IsModerator")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSuperUser")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Guid");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.VariableInInterpretation", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("InterpretationId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("SchemaVariableId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("ValueMax")
                        .HasColumnType("numeric");

                    b.Property<decimal>("ValueMin")
                        .HasColumnType("numeric");

                    b.HasKey("Guid");

                    b.HasIndex("InterpretationId");

                    b.HasIndex("SchemaVariableId");

                    b.ToTable("VariableInInterpretations");
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.ClosedQuestion", b =>
                {
                    b.HasBaseType("AntropoPollWebApi.Core.Models.BaseQuestion");

                    b.Property<int>("MaxCountChoice")
                        .HasColumnType("integer");

                    b.Property<int>("MinCountChoice")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.Answer", b =>
                {
                    b.HasOne("AntropoPollWebApi.Core.Models.BaseQuestion", "BaseQuestion")
                        .WithMany("Answers")
                        .HasForeignKey("BaseQuestionGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.BaseQuestion", b =>
                {
                    b.HasOne("AntropoPollWebApi.Core.Models.Schema", "Schema")
                        .WithMany("BaseQuestions")
                        .HasForeignKey("SchemaId");

                    b.HasOne("AntropoPollWebApi.Core.Models.SchemaVariable", "SchemaVariable")
                        .WithMany("BaseQuestions")
                        .HasForeignKey("SchemaVariableId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.Event", b =>
                {
                    b.HasOne("AntropoPollWebApi.Core.Models.Schema", "Schema")
                        .WithMany()
                        .HasForeignKey("SchemaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AntropoPollWebApi.Core.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.Interpretation", b =>
                {
                    b.HasOne("AntropoPollWebApi.Core.Models.Schema", "Schema")
                        .WithMany()
                        .HasForeignKey("SchemaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.Invite", b =>
                {
                    b.HasOne("AntropoPollWebApi.Core.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.ReportTemplate", b =>
                {
                    b.HasOne("AntropoPollWebApi.Core.Models.Schema", "Schema")
                        .WithMany()
                        .HasForeignKey("SchemaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.Result", b =>
                {
                    b.HasOne("AntropoPollWebApi.Core.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AntropoPollWebApi.Core.Models.Invite", "Invite")
                        .WithMany()
                        .HasForeignKey("InviteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.ResultInterpretation", b =>
                {
                    b.HasOne("AntropoPollWebApi.Core.Models.Interpretation", "Interpretation")
                        .WithMany()
                        .HasForeignKey("InterpretationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AntropoPollWebApi.Core.Models.Result", "Result")
                        .WithMany("ResultInterpretations")
                        .HasForeignKey("ResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.ResultQuestion", b =>
                {
                    b.HasOne("AntropoPollWebApi.Core.Models.Answer", "Answer")
                        .WithMany()
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AntropoPollWebApi.Core.Models.BaseQuestion", "BaseQuestion")
                        .WithMany()
                        .HasForeignKey("BaseQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AntropoPollWebApi.Core.Models.Result", "Result")
                        .WithMany("ResultQuestions")
                        .HasForeignKey("ResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.ResultTemplate", b =>
                {
                    b.HasOne("AntropoPollWebApi.Core.Models.ReportTemplate", "ReportTemplate")
                        .WithMany("ResultTemplates")
                        .HasForeignKey("ReportTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AntropoPollWebApi.Core.Models.Result", "Result")
                        .WithMany("ResultTemplates")
                        .HasForeignKey("ResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.SchemaVariable", b =>
                {
                    b.HasOne("AntropoPollWebApi.Core.Models.Schema", "Schema")
                        .WithMany("SchemaVariables")
                        .HasForeignKey("SchemaId");
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.SystemVariableReport", b =>
                {
                    b.HasOne("AntropoPollWebApi.Core.Models.Invite", "Invite")
                        .WithMany()
                        .HasForeignKey("InviteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AntropoPollWebApi.Core.Models.SchemaVariable", "SchemaVariable")
                        .WithMany()
                        .HasForeignKey("SchemaVariableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.VariableInInterpretation", b =>
                {
                    b.HasOne("AntropoPollWebApi.Core.Models.Interpretation", "Interpretation")
                        .WithMany("VariableInInterpretations")
                        .HasForeignKey("InterpretationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AntropoPollWebApi.Core.Models.SchemaVariable", "SchemaVariable")
                        .WithMany()
                        .HasForeignKey("SchemaVariableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
