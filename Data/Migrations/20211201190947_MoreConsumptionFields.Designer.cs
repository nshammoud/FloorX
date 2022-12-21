﻿// <auto-generated />
using System;
using KQF.Floor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KQF.Floor.Web.Data.Migrations
{
    [DbContext(typeof(FloorDataContext))]
    [Migration("20211201190947_MoreConsumptionFields")]
    partial class MoreConsumptionFields
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KQF.Floor.Data.Models.ConsumptionItemTransaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContainerNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DatePosted")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPosted")
                        .HasColumnType("bit");

                    b.Property<string>("ItemDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LocationCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LotNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MixerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParentItemNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PostedByUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductionOrderNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UoM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContainerNumber");

                    b.HasIndex("IsPosted");

                    b.HasIndex("ItemNumber");

                    b.HasIndex("ParentItemNumber");

                    b.HasIndex("ProductionOrderNumber");

                    b.ToTable("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
