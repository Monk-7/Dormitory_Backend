using System;
using System.Collections.Generic;
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
                name: "CodeRoom",
                columns: table => new
                {
                    idCodeRoom = table.Column<string>(type: "text", nullable: false),
                    idRoom = table.Column<string>(type: "text", nullable: true),
                    codeRoom = table.Column<string>(type: "text", nullable: false),
                    timesTamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeRoom", x => x.idCodeRoom);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    idComment = table.Column<string>(type: "text", nullable: false),
                    idCommunity = table.Column<string>(type: "text", nullable: true),
                    idUser = table.Column<string>(type: "text", nullable: true),
                    details = table.Column<string>(type: "text", nullable: false),
                    timesTamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.idComment);
                });

            migrationBuilder.CreateTable(
                name: "Community",
                columns: table => new
                {
                    idCommunity = table.Column<string>(type: "text", nullable: false),
                    idUser = table.Column<string>(type: "text", nullable: true),
                    idDormitory = table.Column<string>(type: "text", nullable: true),
                    category = table.Column<string>(type: "text", nullable: false),
                    details = table.Column<string>(type: "text", nullable: false),
                    imgFilePath = table.Column<List<string>>(type: "text[]", nullable: true),
                    timesTamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Community", x => x.idCommunity);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    idContract = table.Column<string>(type: "text", nullable: false),
                    idRoom = table.Column<string>(type: "text", nullable: false),
                    pdfFileName = table.Column<string>(type: "text", nullable: false),
                    timesTamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.idContract);
                });

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

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    idInvoice = table.Column<string>(type: "text", nullable: false),
                    idRoom = table.Column<string>(type: "text", nullable: true),
                    roomName = table.Column<int>(type: "integer", nullable: false),
                    roomPrice = table.Column<int>(type: "integer", nullable: false),
                    electricityPrice = table.Column<int>(type: "integer", nullable: false),
                    waterPrice = table.Column<int>(type: "integer", nullable: false),
                    electricityUnit = table.Column<int>(type: "integer", nullable: false),
                    waterUnit = table.Column<int>(type: "integer", nullable: false),
                    furniturePrice = table.Column<int>(type: "integer", nullable: false),
                    internetPrice = table.Column<int>(type: "integer", nullable: false),
                    parkingPrice = table.Column<int>(type: "integer", nullable: false),
                    other = table.Column<int>(type: "integer", nullable: false),
                    total = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: false),
                    statusShow = table.Column<bool>(type: "boolean", nullable: false),
                    dueDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    timesTamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.idInvoice);
                });

            migrationBuilder.CreateTable(
                name: "Meter",
                columns: table => new
                {
                    idMeter = table.Column<string>(type: "text", nullable: false),
                    idDormitory = table.Column<string>(type: "text", nullable: true),
                    idBuilding = table.Column<string>(type: "text", nullable: true),
                    dormitoryName = table.Column<string>(type: "text", nullable: false),
                    buildingName = table.Column<string>(type: "text", nullable: false),
                    timesTamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meter", x => x.idMeter);
                });

            migrationBuilder.CreateTable(
                name: "Notify",
                columns: table => new
                {
                    idNotify = table.Column<string>(type: "text", nullable: false),
                    idUser = table.Column<string>(type: "text", nullable: true),
                    category = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    details = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: false),
                    timesTamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notify", x => x.idNotify);
                });

            migrationBuilder.CreateTable(
                name: "Problem",
                columns: table => new
                {
                    idProblem = table.Column<string>(type: "text", nullable: false),
                    idRoom = table.Column<string>(type: "text", nullable: true),
                    idUser = table.Column<string>(type: "text", nullable: true),
                    category = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    details = table.Column<string>(type: "text", nullable: false),
                    timesTamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Problem", x => x.idProblem);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    idRoom = table.Column<string>(type: "text", nullable: false),
                    idBuilding = table.Column<string>(type: "text", nullable: true),
                    roomName = table.Column<int>(type: "integer", nullable: false),
                    roomPrice = table.Column<int>(type: "integer", nullable: false),
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
                    profile = table.Column<string>(type: "text", nullable: true),
                    token = table.Column<string>(type: "text", nullable: true),
                    timesTamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeterRoom",
                columns: table => new
                {
                    idMeterRoom = table.Column<string>(type: "text", nullable: false),
                    idRoom = table.Column<string>(type: "text", nullable: false),
                    roomName = table.Column<int>(type: "integer", nullable: false),
                    electricity = table.Column<int>(type: "integer", nullable: true),
                    water = table.Column<int>(type: "integer", nullable: true),
                    MeteridMeter = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeterRoom", x => x.idMeterRoom);
                    table.ForeignKey(
                        name: "FK_MeterRoom_Meter_MeteridMeter",
                        column: x => x.MeteridMeter,
                        principalTable: "Meter",
                        principalColumn: "idMeter");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeterRoom_MeteridMeter",
                table: "MeterRoom",
                column: "MeteridMeter");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "CodeRoom");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Community");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "Dormitory");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "MeterRoom");

            migrationBuilder.DropTable(
                name: "Notify");

            migrationBuilder.DropTable(
                name: "Problem");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Meter");
        }
    }
}
