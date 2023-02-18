using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_v3.Migrations
{
    /// <inheritdoc />
    public partial class migration_V4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Films_Directors_directorId",
                table: "Films");

            migrationBuilder.AddForeignKey(
                name: "FK_Films_Directors_directorId",
                table: "Films",
                column: "directorId",
                principalTable: "Directors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Films_Directors_directorId",
                table: "Films");

            migrationBuilder.AddForeignKey(
                name: "FK_Films_Directors_directorId",
                table: "Films",
                column: "directorId",
                principalTable: "Directors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
