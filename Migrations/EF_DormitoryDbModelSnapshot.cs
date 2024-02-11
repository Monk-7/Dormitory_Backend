﻿// <auto-generated />
using System;
using DormitoryAPI.EFcore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DormitoryAPI.Migrations
{
    [DbContext(typeof(EF_DormitoryDb))]
    partial class EF_DormitoryDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DormitoryAPI.Models.Building", b =>
                {
                    b.Property<string>("idBuilding")
                        .HasColumnType("text");

                    b.Property<string>("buildingName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("electricalPrice")
                        .HasColumnType("integer");

                    b.Property<string>("idDormitory")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("timesTamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("waterPrice")
                        .HasColumnType("integer");

                    b.HasKey("idBuilding");

                    b.ToTable("Building");
                });

            modelBuilder.Entity("DormitoryAPI.Models.CodeRoom", b =>
                {
                    b.Property<string>("idCodeRoom")
                        .HasColumnType("text");

                    b.Property<string>("codeRoom")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("idRoom")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("timesTamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("idCodeRoom");

                    b.ToTable("CodeRoom");
                });

            modelBuilder.Entity("DormitoryAPI.Models.Community", b =>
                {
                    b.Property<string>("idCommunity")
                        .HasColumnType("text");

                    b.Property<string>("category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("details")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("idDormitory")
                        .HasColumnType("text");

                    b.Property<string>("idUser")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("timesTamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("idCommunity");

                    b.ToTable("Community");
                });

            modelBuilder.Entity("DormitoryAPI.Models.Dormitory", b =>
                {
                    b.Property<string>("idDormitory")
                        .HasColumnType("text");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("district")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("dormitoryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("idOwner")
                        .HasColumnType("text");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("postalCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("province")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("timesTamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("idDormitory");

                    b.ToTable("Dormitory");
                });

            modelBuilder.Entity("DormitoryAPI.Models.Invoice", b =>
                {
                    b.Property<string>("idInvoice")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("dueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("electricityPrice")
                        .HasColumnType("integer");

                    b.Property<int>("furniturePrice")
                        .HasColumnType("integer");

                    b.Property<string>("idRoom")
                        .HasColumnType("text");

                    b.Property<int>("internetPrice")
                        .HasColumnType("integer");

                    b.Property<int>("other")
                        .HasColumnType("integer");

                    b.Property<int>("parkingPrice")
                        .HasColumnType("integer");

                    b.Property<int>("roomName")
                        .HasColumnType("integer");

                    b.Property<int>("roomPrice")
                        .HasColumnType("integer");

                    b.Property<bool>("status")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("timesTamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("total")
                        .HasColumnType("integer");

                    b.Property<int>("waterPrice")
                        .HasColumnType("integer");

                    b.HasKey("idInvoice");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("DormitoryAPI.Models.Meter", b =>
                {
                    b.Property<string>("idMeter")
                        .HasColumnType("text");

                    b.Property<string>("buildingName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("dormitoryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("idBuilding")
                        .HasColumnType("text");

                    b.Property<string>("idDormitory")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("timesTamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("idMeter");

                    b.ToTable("Meter");
                });

            modelBuilder.Entity("DormitoryAPI.Models.MeterRoom", b =>
                {
                    b.Property<string>("idMeterRoom")
                        .HasColumnType("text");

                    b.Property<string>("MeteridMeter")
                        .HasColumnType("text");

                    b.Property<int?>("electricity")
                        .HasColumnType("integer");

                    b.Property<string>("idRoom")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("roomName")
                        .HasColumnType("integer");

                    b.Property<int?>("water")
                        .HasColumnType("integer");

                    b.HasKey("idMeterRoom");

                    b.HasIndex("MeteridMeter");

                    b.ToTable("MeterRoom");
                });

            modelBuilder.Entity("DormitoryAPI.Models.Problem", b =>
                {
                    b.Property<string>("idProblem")
                        .HasColumnType("text");

                    b.Property<string>("category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("details")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("idRoom")
                        .HasColumnType("text");

                    b.Property<string>("idUser")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("timesTamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("idProblem");

                    b.ToTable("Problem");
                });

            modelBuilder.Entity("DormitoryAPI.Models.Room", b =>
                {
                    b.Property<string>("idRoom")
                        .HasColumnType("text");

                    b.Property<int>("furniturePrice")
                        .HasColumnType("integer");

                    b.Property<string>("idBuilding")
                        .HasColumnType("text");

                    b.Property<int>("internetPrice")
                        .HasColumnType("integer");

                    b.Property<int>("parkingPrice")
                        .HasColumnType("integer");

                    b.Property<int>("roomName")
                        .HasColumnType("integer");

                    b.Property<int>("roomPrice")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("timesTamp")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("idRoom");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("DormitoryAPI.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("IdRoom")
                        .HasColumnType("text");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("lastname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("passwordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("phonenumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("timesTamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("token")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("DormitoryAPI.Models.MeterRoom", b =>
                {
                    b.HasOne("DormitoryAPI.Models.Meter", null)
                        .WithMany("meterRoomAll")
                        .HasForeignKey("MeteridMeter");
                });

            modelBuilder.Entity("DormitoryAPI.Models.Meter", b =>
                {
                    b.Navigation("meterRoomAll");
                });
#pragma warning restore 612, 618
        }
    }
}
