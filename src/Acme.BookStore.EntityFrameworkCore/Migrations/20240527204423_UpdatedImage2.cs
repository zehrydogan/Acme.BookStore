using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acme.BookStore.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedImage2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "AppMovies");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageContent",
                table: "AppMovies",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageContent",
                table: "AppMovies");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "AppMovies",
                type: "varbinary(4000)",
                maxLength: 4000,
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
