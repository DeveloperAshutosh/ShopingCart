﻿// <auto-generated />
using AshZoneModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AshZoneModels.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    partial class StoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AshZoneModels.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImagePath")
                        .HasColumnName("ImagePath")
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnName("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<int>("Price")
                        .HasColumnName("Price")
                        .HasColumnType("int");

                    b.Property<string>("ProductDescription")
                        .HasColumnName("ProductDescription")
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnName("ProductName")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ProductType")
                        .IsRequired()
                        .HasColumnName("ProductType")
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Quantity")
                        .HasColumnName("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
