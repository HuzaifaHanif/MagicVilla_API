﻿// <auto-generated />
using MagicVilla_VillaAPI.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240318070203_seedVillaTable")]
    partial class seedVillaTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MagicVilla_VillaAPI.Models.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<int>("Sqft")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Details = "hello my name is huzaifa",
                            Name = "Huzaifa",
                            Rate = 123.5,
                            Sqft = 200
                        },
                        new
                        {
                            Id = 2,
                            Details = "hello my name is Zeeshan",
                            Name = "Zeeshan",
                            Rate = 200.0,
                            Sqft = 400
                        },
                        new
                        {
                            Id = 3,
                            Details = "hello my name is moosa",
                            Name = "Moosa",
                            Rate = 123.5,
                            Sqft = 200
                        },
                        new
                        {
                            Id = 4,
                            Details = "hello my name is Faisal",
                            Name = "Faisal",
                            Rate = 200.0,
                            Sqft = 400
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
