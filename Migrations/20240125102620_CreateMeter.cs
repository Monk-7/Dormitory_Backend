using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormitoryAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateMeter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meter",
                columns: table => new
                {
                    idMeter = table.Column<string>(type: "text", nullable: false),
                    idDormitory = table.Column<string>(type: "text", nullable: true),
                    buildingName = table.Column<string>(type: "text", nullable: false),
                    timesTamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meter", x => x.idMeter);
                });

            migrationBuilder.CreateTable(
                name: "MeterRoom",
                columns: table => new
                {
                    idMeterRoom = table.Column<string>(type: "text", nullable: false),
                    idRoom = table.Column<string>(type: "text", nullable: false),
                    roomName = table.Column<string>(type: "text", nullable: false),
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
                name: "MeterRoom");

            migrationBuilder.DropTable(
                name: "Meter");
        }
    }
}
