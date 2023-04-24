using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Worky.Migrations
{
    public partial class changeshemedocs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Pages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParrentId",
                table: "Pages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "ParrentId",
                table: "Pages");
        }
    }
}
