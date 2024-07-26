using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPBoardWebApi.Migrations
{
    /// <inheritdoc />
    public partial class nameunique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableElements_Users_AssignedUserId",
                table: "TableElements");

            migrationBuilder.DropIndex(
                name: "IX_TableElements_AssignedUserId",
                table: "TableElements");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Name",
                table: "Users",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Name",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_TableElements_AssignedUserId",
                table: "TableElements",
                column: "AssignedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TableElements_Users_AssignedUserId",
                table: "TableElements",
                column: "AssignedUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
