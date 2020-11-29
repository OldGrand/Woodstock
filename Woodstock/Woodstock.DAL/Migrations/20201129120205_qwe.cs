using Microsoft.EntityFrameworkCore.Migrations;

namespace Woodstock.DAL.Migrations
{
    public partial class qwe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Count",
                table: "Orders",
                newName: "TotalCount");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "OrderWatchLinks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "OrderWatchLinks");

            migrationBuilder.RenameColumn(
                name: "TotalCount",
                table: "Orders",
                newName: "Count");
        }
    }
}
