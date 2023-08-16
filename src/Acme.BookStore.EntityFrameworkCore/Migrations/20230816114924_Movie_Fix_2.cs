using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acme.BookStore.Migrations
{
    /// <inheritdoc />
    public partial class MovieFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppMovies_AppAuthors_DirectorId",
                table: "AppMovies");

            migrationBuilder.DropIndex(
                name: "IX_AppMovies_DirectorId",
                table: "AppMovies");

            migrationBuilder.DropColumn(
                name: "DirectorId",
                table: "AppMovies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DirectorId",
                table: "AppMovies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppMovies_DirectorId",
                table: "AppMovies",
                column: "DirectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppMovies_AppAuthors_DirectorId",
                table: "AppMovies",
                column: "DirectorId",
                principalTable: "AppAuthors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
