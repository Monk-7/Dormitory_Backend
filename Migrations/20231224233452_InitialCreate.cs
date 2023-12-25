using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormitoryAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    idBuilding = table.Column<string>(type: "text", nullable: false),
                    idDormitory = table.Column<string>(type: "text", nullable: true),
                    buildingName = table.Column<string>(type: "text", nullable: false),
                    waterPrice = table.Column<int>(type: "integer", nullable: false),
                    electricalPrice = table.Column<int>(type: "integer", nullable: false),
                    timesTamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.idBuilding);
                });

            migrationBuilder.CreateTable(
                name: "Dormitory",
                columns: table => new
                {
                    idDormitory = table.Column<string>(type: "text", nullable: false),
                    idOwner = table.Column<string>(type: "text", nullable: true),
                    dormitoryName = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    phoneNumber = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    timesTamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dormitory", x => x.idDormitory);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    idRoom = table.Column<string>(type: "text", nullable: false),
                    idBuilding = table.Column<string>(type: "text", nullable: true),
                    roomName = table.Column<string>(type: "text", nullable: false),
                    furniturePrice = table.Column<int>(type: "integer", nullable: false),
                    internetPrice = table.Column<int>(type: "integer", nullable: false),
                    parkingPrice = table.Column<int>(type: "integer", nullable: false),
                    timesTamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.idRoom);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    IdRoom = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    lastname = table.Column<string>(type: "text", nullable: false),
                    passwordHash = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<string>(type: "text", nullable: false),
                    phonenumber = table.Column<string>(type: "text", nullable: false),
                    token = table.Column<string>(type: "text", nullable: true),
                    timesTamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "Dormitory");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
