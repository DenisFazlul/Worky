using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Worky.Migrations.ProjectDb
{
    public partial class EditTaskFile1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TaskFile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "TaskFile");
        }
    }
}
