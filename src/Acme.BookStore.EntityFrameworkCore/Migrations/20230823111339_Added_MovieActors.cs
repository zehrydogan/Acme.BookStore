using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acme.BookStore.Migrations
{
    /// <inheritdoc />
    public partial class AddedMovieActors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppMovies_AppActors_ActorId",
                table: "AppMovies");

            migrationBuilder.DropIndex(
                name: "IX_AppMovies_ActorId",
                table: "AppMovies");

            migrationBuilder.DropColumn(
                name: "ActorId",
                table: "AppMovies");

            migrationBuilder.CreateTable(
                name: "AppMovieActors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMovieActors", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppMovieActors");

            migrationBuilder.AddColumn<Guid>(
                name: "ActorId",
                table: "AppMovies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppMovies_ActorId",
                table: "AppMovies",
                column: "ActorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppMovies_AppActors_ActorId",
                table: "AppMovies",
                column: "ActorId",
                principalTable: "AppActors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
