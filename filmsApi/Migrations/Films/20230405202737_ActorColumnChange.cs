using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace filmsApi.Migrations.Films
{
    /// <inheritdoc />
    public partial class ActorColumnChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Actor");

            migrationBuilder.AlterColumn<int>(
                name: "Length",
                table: "Movie",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "YearOfBirth",
                table: "Actor",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearOfBirth",
                table: "Actor");

            migrationBuilder.AlterColumn<string>(
                name: "Length",
                table: "Movie",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "Actor",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }
    }
}
