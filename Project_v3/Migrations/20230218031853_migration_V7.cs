using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_v3.Migrations
{
    /// <inheritdoc />
    public partial class migration_V7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Films_Directors_directorId",
                table: "Films");

            migrationBuilder.DropIndex(
                name: "IX_Films_directorId",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "directorId",
                table: "Films");

            migrationBuilder.AddColumn<string>(
                name: "Directorfullname",
                table: "Films",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "fullname",
                table: "Directors",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Directors_fullname",
                table: "Directors",
                column: "fullname");

            migrationBuilder.CreateIndex(
                name: "IX_Films_Directorfullname",
                table: "Films",
                column: "Directorfullname");

            migrationBuilder.AddForeignKey(
                name: "FK_Films_Directors_Directorfullname",
                table: "Films",
                column: "Directorfullname",
                principalTable: "Directors",
                principalColumn: "fullname",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Films_Directors_Directorfullname",
                table: "Films");

            migrationBuilder.DropIndex(
                name: "IX_Films_Directorfullname",
                table: "Films");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Directors_fullname",
                table: "Directors");

            migrationBuilder.DropColumn(
                name: "Directorfullname",
                table: "Films");

            migrationBuilder.AddColumn<int>(
                name: "directorId",
                table: "Films",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "fullname",
                table: "Directors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Films_directorId",
                table: "Films",
                column: "directorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Films_Directors_directorId",
                table: "Films",
                column: "directorId",
                principalTable: "Directors",
                principalColumn: "Id");
        }
    }
}
