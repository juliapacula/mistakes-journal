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
    [Migration("20210726165036_AddedMistakeAndTip")]
    partial class AddedMistakeAndTip
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Mistakes.Journal.Api.Logic.Mistakes.Models.Mistake", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("Goal")
                        .HasColumnName("goal")
                        .HasColumnType("text");

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

                    b.Property<Guid>("MistakeId")
                        .HasColumnName("mistake_id")
                        .HasColumnType("uuid");

                    b.HasKey("Id")
                        .HasName("pk_tip");

                    b.HasIndex("MistakeId")
                        .HasName("ix_tip_mistake_id");

                    b.ToTable("tip");
                });

            modelBuilder.Entity("Mistakes.Journal.Api.Logic.Mistakes.Models.Tip", b =>
                {
                    b.HasOne("Mistakes.Journal.Api.Logic.Mistakes.Models.Mistake", "Mistake")
                        .WithMany("Tips")
                        .HasForeignKey("MistakeId")
                        .HasConstraintName("fk_tip_mistake_mistake_id1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
