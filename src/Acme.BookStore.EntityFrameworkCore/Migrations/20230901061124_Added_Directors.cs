using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acme.BookStore.Migrations
{
    /// <inheritdoc />
    public partial class AddedDirectors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_AppMovies_AppDirectors_DirectorId",
                table: "AppMovies",
                column: "DirectorId",
                principalTable: "AppDirectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppMovies_AppDirectors_DirectorId",
                table: "AppMovies");

            migrationBuilder.DropIndex(
                name: "IX_AppMovies_DirectorId",
                table: "AppMovies");

            migrationBuilder.DropColumn(
                name: "DirectorId",
                table: "AppMovies");
        }
    }
}
