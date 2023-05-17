using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Worky.Migrations
{
    public partial class addwhatchcounttodocpage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WhatchCount",
                table: "Pages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WhatchCount",
                table: "Pages");
        }
    }
}
