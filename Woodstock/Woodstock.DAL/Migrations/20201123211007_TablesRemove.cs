using Microsoft.EntityFrameworkCore.Migrations;

namespace Woodstock.DAL.Migrations
{
    public partial class TablesRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Manufacturers_Countries_CountryId",
                table: "Manufacturers");

            migrationBuilder.DropForeignKey(
                name: "FK_Mechanisms_MechanismTypes_MechanismTypeId",
                table: "Mechanisms");

            migrationBuilder.DropForeignKey(
                name: "FK_Straps_Colors_ColorId",
                table: "Straps");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "MechanismTypes");

            migrationBuilder.DropIndex(
                name: "IX_Mechanisms_MechanismTypeId",
                table: "Mechanisms");

            migrationBuilder.DropIndex(
                name: "IX_Manufacturers_CountryId",
                table: "Manufacturers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Colors",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "MechanismTypeId",
                table: "Mechanisms");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Manufacturers");

            migrationBuilder.RenameTable(
                name: "Colors",
                newName: "MyProperty");

            migrationBuilder.AddColumn<string>(
                name: "MechanismType",
                table: "Mechanisms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Manufacturers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MyProperty",
                table: "MyProperty",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Straps_MyProperty_ColorId",
                table: "Straps",
                column: "ColorId",
                principalTable: "MyProperty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Straps_MyProperty_ColorId",
                table: "Straps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MyProperty",
                table: "MyProperty");

            migrationBuilder.DropColumn(
                name: "MechanismType",
                table: "Mechanisms");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Manufacturers");

            migrationBuilder.RenameTable(
                name: "MyProperty",
                newName: "Colors");

            migrationBuilder.AddColumn<int>(
                name: "MechanismTypeId",
                table: "Mechanisms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Manufacturers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Colors",
                table: "Colors",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MechanismTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MechanismTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mechanisms_MechanismTypeId",
                table: "Mechanisms",
                column: "MechanismTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_CountryId",
                table: "Manufacturers",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Manufacturers_Countries_CountryId",
                table: "Manufacturers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mechanisms_MechanismTypes_MechanismTypeId",
                table: "Mechanisms",
                column: "MechanismTypeId",
                principalTable: "MechanismTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Straps_Colors_ColorId",
                table: "Straps",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
