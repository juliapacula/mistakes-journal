﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mistakes.Journal.Api;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Mistakes.Journal.Api.Migrations
{
    [DbContext(typeof(MistakesJournalContext))]
    [Migration("20210828141250_MistakesCanBeSolved")]
    partial class MistakesCanBeSolved
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Mistakes.Journal.Api.Logic.Mistakes.Models.Label", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnName("color")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_label");

                    b.ToTable("label");
                });

            modelBuilder.Entity("Mistakes.Journal.Api.Logic.Mistakes.Models.Mistake", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("Goal")
                        .HasColumnName("goal")
                        .HasColumnType("text");

                    b.Property<bool>("IsSolved")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("is_solved")
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasColumnName("priority")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_mistake");

                    b.ToTable("mistake");
                });

            modelBuilder.Entity("Mistakes.Journal.Api.Logic.Mistakes.Models.MistakeLabel", b =>
                {
                    b.Property<Guid>("MistakeId")
                        .HasColumnName("mistake_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("LabelId")
                        .HasColumnName("label_id")
                        .HasColumnType("uuid");

                    b.HasKey("MistakeId", "LabelId")
                        .HasName("pk_mistake_label");

                    b.HasIndex("LabelId")
                        .HasName("ix_mistake_label_label_id");

                    b.ToTable("mistake_label");
                });

            modelBuilder.Entity("Mistakes.Journal.Api.Logic.Mistakes.Models.Repetition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateTime")
                        .HasColumnName("date_time")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("MistakeId")
                        .HasColumnName("mistake_id")
                        .HasColumnType("uuid");

                    b.HasKey("Id")
                        .HasName("pk_repetition");

                    b.HasIndex("MistakeId")
                        .HasName("ix_repetition_mistake_id");

                    b.ToTable("repetition");
                });

            modelBuilder.Entity("Mistakes.Journal.Api.Logic.Mistakes.Models.Tip", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnName("content")
                        .HasColumnType("text");

                    b.Property<Guid?>("MistakeId")
                        .HasColumnName("mistake_id")
                        .HasColumnType("uuid");

                    b.HasKey("Id")
                        .HasName("pk_tip");

                    b.HasIndex("MistakeId")
                        .HasName("ix_tip_mistake_id");

                    b.ToTable("tip");
                });

            modelBuilder.Entity("Mistakes.Journal.Api.Logic.Mistakes.Models.MistakeLabel", b =>
                {
                    b.HasOne("Mistakes.Journal.Api.Logic.Mistakes.Models.Label", "Label")
                        .WithMany("MistakeLabels")
                        .HasForeignKey("LabelId")
                        .HasConstraintName("fk_mistake_label_label_label_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Mistakes.Journal.Api.Logic.Mistakes.Models.Mistake", "Mistake")
                        .WithMany("MistakeLabels")
                        .HasForeignKey("MistakeId")
                        .HasConstraintName("fk_mistake_label_mistake_mistake_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Mistakes.Journal.Api.Logic.Mistakes.Models.Repetition", b =>
                {
                    b.HasOne("Mistakes.Journal.Api.Logic.Mistakes.Models.Mistake", "Mistake")
                        .WithMany("Repetitions")
                        .HasForeignKey("MistakeId")
                        .HasConstraintName("fk_repetition_mistake_mistake_id1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Mistakes.Journal.Api.Logic.Mistakes.Models.Tip", b =>
                {
                    b.HasOne("Mistakes.Journal.Api.Logic.Mistakes.Models.Mistake", "Mistake")
                        .WithMany("Tips")
                        .HasForeignKey("MistakeId")
                        .HasConstraintName("fk_tip_mistake_mistake_id1");
                });
#pragma warning restore 612, 618
        }
    }
}