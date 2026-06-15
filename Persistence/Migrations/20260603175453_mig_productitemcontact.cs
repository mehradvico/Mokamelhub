using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_productitemcontact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProductIdItemId",
                table: "ContactUses",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductItemId",
                table: "ContactUses",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactUses_ProductItemId",
                table: "ContactUses",
                column: "ProductItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactUses_ProductItems_ProductItemId",
                table: "ContactUses",
                column: "ProductItemId",
                principalTable: "ProductItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactUses_ProductItems_ProductItemId",
                table: "ContactUses");

            migrationBuilder.DropIndex(
                name: "IX_ContactUses_ProductItemId",
                table: "ContactUses");

            migrationBuilder.DropColumn(
                name: "ProductIdItemId",
                table: "ContactUses");

            migrationBuilder.DropColumn(
                name: "ProductItemId",
                table: "ContactUses");
        }
    }
}
