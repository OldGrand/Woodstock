using Microsoft.EntityFrameworkCore.Migrations;

namespace Woodstock.DAL.Migrations
{
    public partial class RenameQuantityToCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalQuantity",
                table: "Orders",
                newName: "Count");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Count",
                table: "Orders",
                newName: "TotalQuantity");
        }
    }
}
