using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace filmsApi.Migrations.Films
{
    /// <inheritdoc />
    public partial class MovieRatingsRelationship3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Rating",
                table: "MovieRating",
                type: "TEXT",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldMaxLength: 4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "MovieRating",
                type: "INTEGER",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 4);
        }
    }
}
