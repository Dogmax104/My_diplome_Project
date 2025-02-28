﻿// <auto-generated />
using System;
using Arctech_Manufaction_Menedgment.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Arctech_Manufaction_Menedgment.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250227104722_migration1")]
    partial class migration1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.13");

            modelBuilder.Entity("Arctech_Manufaction_Menedgment.Models.ModelFileClient", b =>
                {
                    b.Property<int>("IdModelFileClient")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateModelFile")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("NameModelFile")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("NameModelFileClient")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ProgectModelIdProjectModel")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdModelFileClient");

                    b.HasIndex("ProgectModelIdProjectModel");

                    b.ToTable("ModelFiles");
                });

            modelBuilder.Entity("Arctech_Manufaction_Menedgment.Models.ProgectModel", b =>
                {
                    b.Property<int>("IdProjectModel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BeginTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClientNameProjectModel")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<byte?>("CoordinationFileProjectModel")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CoordinationTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("NameProjectModel")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NotesProjectModel")
                        .HasColumnType("TEXT");

                    b.Property<bool?>("OrderInManufaction")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("StatusOrder")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdProjectModel");

                    b.ToTable("ProgectModels");
                });

            modelBuilder.Entity("Arctech_Manufaction_Menedgment.Models.UserArctech", b =>
                {
                    b.Property<string>("NameUser")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordUser")
                        .HasColumnType("TEXT");

                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RoleUser")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("NameUser", "PasswordUser");

                    b.ToTable("UserArctechs");
                });

            modelBuilder.Entity("Arctech_Manufaction_Menedgment.Models.ModelFileClient", b =>
                {
                    b.HasOne("Arctech_Manufaction_Menedgment.Models.ProgectModel", null)
                        .WithMany("ClientFileProjectModel")
                        .HasForeignKey("ProgectModelIdProjectModel");
                });

            modelBuilder.Entity("Arctech_Manufaction_Menedgment.Models.ProgectModel", b =>
                {
                    b.Navigation("ClientFileProjectModel");
                });
#pragma warning restore 612, 618
        }
    }
}
