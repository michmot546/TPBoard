using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPBoardWebApi.Migrations
{
    /// <inheritdoc />
    public partial class test3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TableElement_Tables_TableId",
                table: "TableElement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TableElement",
                table: "TableElement");

            migrationBuilder.RenameTable(
                name: "TableElement",
                newName: "Elements");

            migrationBuilder.RenameIndex(
                name: "IX_TableElement_TableId",
                table: "Elements",
                newName: "IX_Elements_TableId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Elements",
                table: "Elements",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Elements_Tables_TableId",
                table: "Elements",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Elements_Tables_TableId",
                table: "Elements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Elements",
                table: "Elements");

            migrationBuilder.RenameTable(
                name: "Elements",
                newName: "TableElement");

            migrationBuilder.RenameIndex(
                name: "IX_Elements_TableId",
                table: "TableElement",
                newName: "IX_TableElement_TableId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableElement",
                table: "TableElement",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TableElement_Tables_TableId",
                table: "TableElement",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
