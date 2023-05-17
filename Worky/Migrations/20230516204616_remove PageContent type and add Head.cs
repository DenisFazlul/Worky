using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Worky.Migrations
{
    public partial class removePageContenttypeandaddHead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "PageContents");

            migrationBuilder.AddColumn<string>(
                name: "Head",
                table: "PageContents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Head",
                table: "PageContents");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "PageContents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
