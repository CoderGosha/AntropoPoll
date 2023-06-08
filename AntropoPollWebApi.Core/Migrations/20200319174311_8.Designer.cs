﻿// <auto-generated />
using System;
using System.Text.Json;
using AntropoPollWebApi.Core.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AntropoPollWebApi.Core.Migrations
{
    [DbContext(typeof(AntropoPollContext))]
    [Migration("20200319174311_8")]
    partial class _8
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<Guid?>("BaseQuestionGuid")
                        .HasColumnType("uuid");

                    b.Property<int>("Index")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("QuestionGuid")
                        .HasColumnType("uuid");

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

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.Result", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<JsonDocument>("FormAnalytics")
                        .HasColumnType("jsonb");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("SchemaId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Guid");

                    b.HasIndex("SchemaId");

                    b.ToTable("Results");
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

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.Schema", b =>
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

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.User", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsModerator")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSuperUser")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastUpdate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Guid");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.ClosedQuestion", b =>
                {
                    b.HasBaseType("AntropoPollWebApi.Core.Models.BaseQuestion");

                    b.Property<int>("MaxCountСhoice")
                        .HasColumnType("integer");

                    b.Property<int>("MinCountСhoice")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.Answer", b =>
                {
                    b.HasOne("AntropoPollWebApi.Core.Models.BaseQuestion", "BaseQuestion")
                        .WithMany("Answers")
                        .HasForeignKey("BaseQuestionGuid");
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.BaseQuestion", b =>
                {
                    b.HasOne("AntropoPollWebApi.Core.Models.Schema", "Schema")
                        .WithMany()
                        .HasForeignKey("SchemaId");

                    b.HasOne("AntropoPollWebApi.Core.Models.SchemaVariable", "SchemaVariable")
                        .WithMany()
                        .HasForeignKey("SchemaVariableId");
                });

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.Result", b =>
                {
                    b.HasOne("AntropoPollWebApi.Core.Models.Schema", "Schema")
                        .WithMany()
                        .HasForeignKey("SchemaId")
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

            modelBuilder.Entity("AntropoPollWebApi.Core.Models.SchemaVariable", b =>
                {
                    b.HasOne("AntropoPollWebApi.Core.Models.Schema", "Schema")
                        .WithMany()
                        .HasForeignKey("SchemaId");
                });
#pragma warning restore 612, 618
        }
    }
}
