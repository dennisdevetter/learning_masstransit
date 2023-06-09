﻿// <auto-generated />
using System;
using LearningMassTransit.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LearningMassTransit.DataAccess.Migrations
{
    [DbContext(typeof(LaraDbContext))]
    partial class LaraDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("lara")
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LearningMassTransit.Domain.Blogging.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BlogId"));

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("BlogId");

                    b.ToTable("Blogs", "lara");
                });

            modelBuilder.Entity("LearningMassTransit.Domain.Blogging.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PostId"));

                    b.Property<int>("BlogId")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<string>("Title")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("PostId");

                    b.HasIndex("BlogId");

                    b.ToTable("Posts", "lara");
                });

            modelBuilder.Entity("LearningMassTransit.Domain.Lara.Wizard", b =>
                {
                    b.Property<Guid>("WizardId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Kind")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("WizardId");

                    b.ToTable("Wizards", "lara");
                });

            modelBuilder.Entity("LearningMassTransit.Domain.Lara.WizardStep", b =>
                {
                    b.Property<Guid>("WizardStepId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("StepData")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StepNr")
                        .HasColumnType("integer");

                    b.Property<string>("StepType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TicketData")
                        .HasColumnType("text");

                    b.Property<string>("TicketId")
                        .HasColumnType("text");

                    b.Property<Guid>("WizardId")
                        .HasColumnType("uuid");

                    b.HasKey("WizardStepId");

                    b.HasIndex("WizardId");

                    b.ToTable("WizardSteps", "lara");
                });

            modelBuilder.Entity("LearningMassTransit.Domain.Blogging.Post", b =>
                {
                    b.HasOne("LearningMassTransit.Domain.Blogging.Blog", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Blog");
                });

            modelBuilder.Entity("LearningMassTransit.Domain.Lara.WizardStep", b =>
                {
                    b.HasOne("LearningMassTransit.Domain.Lara.Wizard", "Wizard")
                        .WithMany("Steps")
                        .HasForeignKey("WizardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wizard");
                });

            modelBuilder.Entity("LearningMassTransit.Domain.Blogging.Blog", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("LearningMassTransit.Domain.Lara.Wizard", b =>
                {
                    b.Navigation("Steps");
                });
#pragma warning restore 612, 618
        }
    }
}
