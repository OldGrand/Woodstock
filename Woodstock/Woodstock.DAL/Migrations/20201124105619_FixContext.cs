using Microsoft.EntityFrameworkCore.Migrations;

namespace Woodstock.DAL.Migrations
{
    public partial class FixContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartWatchLink");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts");

            migrationBuilder.AddColumn<int>(
                name: "WatchId",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_WatchId",
                table: "ShoppingCarts",
                column: "WatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Watches_WatchId",
                table: "ShoppingCarts",
                column: "WatchId",
                principalTable: "Watches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Watches_WatchId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_WatchId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "WatchId",
                table: "ShoppingCarts");

            migrationBuilder.CreateTable(
                name: "CartWatchLink",
                columns: table => new
                {
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false),
                    WatchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartWatchLink", x => new { x.ShoppingCartId, x.WatchId });
                    table.ForeignKey(
                        name: "FK_CartWatchLink_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartWatchLink_Watches_WatchId",
                        column: x => x.WatchId,
                        principalTable: "Watches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartWatchLink_WatchId",
                table: "CartWatchLink",
                column: "WatchId");
        }
    }
}
