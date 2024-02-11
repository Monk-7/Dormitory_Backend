using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormitoryAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    furniturePrice = table.Column<int>(type: "integer", nullable: false),
                    internetPrice = table.Column<int>(type: "integer", nullable: false),
                    parkingPrice = table.Column<int>(type: "integer", nullable: false),
                    other = table.Column<int>(type: "integer", nullable: false),
                    total = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<bool>(type: "boolean", nullable: false),
                    dueDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    timesTamp = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.idInvoice);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoice");
        }
    }
}
