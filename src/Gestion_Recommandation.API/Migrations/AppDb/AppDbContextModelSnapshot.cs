﻿// <auto-generated />
using System;
using Gestion_Recommandation.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gestion_Recommandation.API.Migrations.AppDb
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Gestion_Recommandation.Shared.Models.Recommandations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateReference")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeLaPart")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ID_User")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentityRecommandation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstructionDRH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Recommandations");
                });

            modelBuilder.Entity("Gestion_Recommandation.Shared.Models.Trace_Recommandations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AgentSaisie")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateSaisie")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ID_Recommandations")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Trace_Recommandations");
                });
#pragma warning restore 612, 618
        }
    }
}