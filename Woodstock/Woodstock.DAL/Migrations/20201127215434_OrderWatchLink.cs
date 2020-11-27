using Microsoft.EntityFrameworkCore.Migrations;

namespace Woodstock.DAL.Migrations
{
    public partial class OrderWatchLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderWatchLink_Orders_OrderId",
                table: "OrderWatchLink");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderWatchLink_Watches_WatchId",
                table: "OrderWatchLink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderWatchLink",
                table: "OrderWatchLink");

            migrationBuilder.RenameTable(
                name: "OrderWatchLink",
                newName: "OrderWatchLinks");

            migrationBuilder.RenameIndex(
                name: "IX_OrderWatchLink_WatchId",
                table: "OrderWatchLinks",
                newName: "IX_OrderWatchLinks_WatchId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrderWatchLinks",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderWatchLinks",
                table: "OrderWatchLinks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderWatchLinks_OrderId",
                table: "OrderWatchLinks",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderWatchLinks_Orders_OrderId",
                table: "OrderWatchLinks",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderWatchLinks_Watches_WatchId",
                table: "OrderWatchLinks",
                column: "WatchId",
                principalTable: "Watches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderWatchLinks_Orders_OrderId",
                table: "OrderWatchLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderWatchLinks_Watches_WatchId",
                table: "OrderWatchLinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderWatchLinks",
                table: "OrderWatchLinks");

            migrationBuilder.DropIndex(
                name: "IX_OrderWatchLinks_OrderId",
                table: "OrderWatchLinks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderWatchLinks");

            migrationBuilder.RenameTable(
                name: "OrderWatchLinks",
                newName: "OrderWatchLink");

            migrationBuilder.RenameIndex(
                name: "IX_OrderWatchLinks_WatchId",
                table: "OrderWatchLink",
                newName: "IX_OrderWatchLink_WatchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderWatchLink",
                table: "OrderWatchLink",
                columns: new[] { "OrderId", "WatchId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderWatchLink_Orders_OrderId",
                table: "OrderWatchLink",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderWatchLink_Watches_WatchId",
                table: "OrderWatchLink",
                column: "WatchId",
                principalTable: "Watches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
