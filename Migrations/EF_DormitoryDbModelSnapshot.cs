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

            modelBuilder.Entity("DormitoryAPI.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

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

                    b.Property<byte[]>("passwordHash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("passwordSalt")
                        .HasColumnType("bytea");

                    b.Property<int>("phonenumber")
                        .HasColumnType("integer");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("token")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
