using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormitoryAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateDorm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dormitory",
                columns: table => new
                {
                    idDormitory = table.Column<string>(type: "text", nullable: false),
                    idOwner = table.Column<string>(type: "text", nullable: true),
                    dormitoryName = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    district = table.Column<string>(type: "text", nullable: false),
                    province = table.Column<string>(type: "text", nullable: false),
                    postalCode = table.Column<string>(type: "text", nullable: false),
                    phoneNumber = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    timesTamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dormitory", x => x.idDormitory);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dormitory");
        }
    }
}
