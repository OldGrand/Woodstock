using Microsoft.EntityFrameworkCore.Migrations;

namespace Woodstock.DAL.Migrations
{
    public partial class StrapWristNullableDisable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Straps_WristSize_WristSizeId",
                table: "Straps");

            migrationBuilder.AlterColumn<int>(
                name: "WristSizeId",
                table: "Straps",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Straps_WristSize_WristSizeId",
                table: "Straps",
                column: "WristSizeId",
                principalTable: "WristSize",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Straps_WristSize_WristSizeId",
                table: "Straps");

            migrationBuilder.AlterColumn<int>(
                name: "WristSizeId",
                table: "Straps",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Straps_WristSize_WristSizeId",
                table: "Straps",
                column: "WristSizeId",
                principalTable: "WristSize",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
