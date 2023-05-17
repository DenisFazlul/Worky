using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Worky.Migrations
{
    public partial class addcontenttypetopagecontent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContentType",
                table: "PageContents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "PageContents");
        }
    }
}
