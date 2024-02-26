using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormitoryAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");
        }
    }
}
