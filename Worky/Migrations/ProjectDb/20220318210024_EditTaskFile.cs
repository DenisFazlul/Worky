using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Worky.Migrations.ProjectDb
{
    public partial class EditTaskFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskFiles",
                table: "TaskFiles");

            migrationBuilder.DropColumn(
                name: "Bytes",
                table: "TaskFiles");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TaskFiles");

            migrationBuilder.RenameTable(
                name: "TaskFiles",
                newName: "TaskFile");

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "TaskFile",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskFile",
                table: "TaskFile",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskFile",
                table: "TaskFile");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "TaskFile");

            migrationBuilder.RenameTable(
                name: "TaskFile",
                newName: "TaskFiles");

            migrationBuilder.AddColumn<byte[]>(
                name: "Bytes",
                table: "TaskFiles",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TaskFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskFiles",
                table: "TaskFiles",
                column: "Id");
        }
    }
}
