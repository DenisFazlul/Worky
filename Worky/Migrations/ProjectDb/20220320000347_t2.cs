using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Worky.Migrations.ProjectDb
{
    public partial class t2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TaskFile_TaskId",
                table: "TaskFile",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskFile_Tasks_TaskId",
                table: "TaskFile",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskFile_Tasks_TaskId",
                table: "TaskFile");

            migrationBuilder.DropIndex(
                name: "IX_TaskFile_TaskId",
                table: "TaskFile");
        }
    }
}
