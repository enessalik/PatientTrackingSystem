﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PatientTrackingSystem.Web.Models;

#nullable disable

namespace PatientTrackingSystem.Web.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230705102813_4")]
    partial class _4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PatientTrackingSystem.Web.Models.Patient", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<DateTime>("birthday")
                        .HasColumnType("Date");

                    b.Property<long>("id_card")
                        .HasColumnType("bigint");

                    b.Property<string>("name_surname")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.HasKey("id");

                    b.HasIndex("id_card")
                        .IsUnique();

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("PatientTrackingSystem.Web.Models.Visit", b =>
                {
                    b.Property<int>("Visit_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Visit_Id"));

                    b.Property<string>("Doctor_Name")
                        .HasColumnType("text");

                    b.Property<int>("Patient_Id")
                        .HasColumnType("integer");

                    b.Property<string>("Patient_Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("Visit_Date")
                        .HasColumnType("timestamp(0) without time zone");

                    b.HasKey("Visit_Id");

                    b.HasIndex("Patient_Id");

                    b.ToTable("Visits");
                });

            modelBuilder.Entity("PatientTrackingSystem.Web.Models.Visit", b =>
                {
                    b.HasOne("PatientTrackingSystem.Web.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("Patient_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });
#pragma warning restore 612, 618
        }
    }
}
