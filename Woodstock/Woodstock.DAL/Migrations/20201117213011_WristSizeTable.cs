using Microsoft.EntityFrameworkCore.Migrations;

namespace Woodstock.DAL.Migrations
{
    public partial class WristSizeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Straps");

            migrationBuilder.AddColumn<int>(
                name: "WristSizeId",
                table: "Straps",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WristSize",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<double>(type: "float", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WristSize", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Straps_WristSizeId",
                table: "Straps",
                column: "WristSizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Straps_WristSize_WristSizeId",
                table: "Straps",
                column: "WristSizeId",
                principalTable: "WristSize",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Straps_WristSize_WristSizeId",
                table: "Straps");

            migrationBuilder.DropTable(
                name: "WristSize");

            migrationBuilder.DropIndex(
                name: "IX_Straps_WristSizeId",
                table: "Straps");

            migrationBuilder.DropColumn(
                name: "WristSizeId",
                table: "Straps");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Straps",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
