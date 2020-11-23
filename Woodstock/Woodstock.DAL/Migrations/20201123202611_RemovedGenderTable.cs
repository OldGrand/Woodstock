using Microsoft.EntityFrameworkCore.Migrations;

namespace Woodstock.DAL.Migrations
{
    public partial class RemovedGenderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Watches_Genders_GenderId",
                table: "Watches");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropIndex(
                name: "IX_Watches_GenderId",
                table: "Watches");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "Watches");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Watches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Watches");

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "Watches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Watches_GenderId",
                table: "Watches",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Watches_Genders_GenderId",
                table: "Watches",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
