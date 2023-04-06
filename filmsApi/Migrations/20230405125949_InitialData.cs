using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace filmsApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", unicode: false, maxLength: 60, nullable: true),
                    Email = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "TEXT", unicode: false, maxLength: 20, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInfo");
        }
    }
}
