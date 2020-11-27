using Microsoft.EntityFrameworkCore.Migrations;

namespace Woodstock.DAL.Migrations
{
    public partial class CartCheckbox : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsChecked",
                table: "ShoppingCarts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsChecked",
                table: "ShoppingCarts");
        }
    }
}
