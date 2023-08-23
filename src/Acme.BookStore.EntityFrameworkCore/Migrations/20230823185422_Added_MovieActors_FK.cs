using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acme.BookStore.Migrations
{
    /// <inheritdoc />
    public partial class AddedMovieActorsFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppMovieActors_ActorId",
                table: "AppMovieActors",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMovieActors_MovieId",
                table: "AppMovieActors",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppMovieActors_AppActors_ActorId",
                table: "AppMovieActors",
                column: "ActorId",
                principalTable: "AppActors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppMovieActors_AppMovies_MovieId",
                table: "AppMovieActors",
                column: "MovieId",
                principalTable: "AppMovies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppMovieActors_AppActors_ActorId",
                table: "AppMovieActors");

            migrationBuilder.DropForeignKey(
                name: "FK_AppMovieActors_AppMovies_MovieId",
                table: "AppMovieActors");

            migrationBuilder.DropIndex(
                name: "IX_AppMovieActors_ActorId",
                table: "AppMovieActors");

            migrationBuilder.DropIndex(
                name: "IX_AppMovieActors_MovieId",
                table: "AppMovieActors");
        }
    }
}
