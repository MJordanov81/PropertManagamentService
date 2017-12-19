using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PropertyManagementService.Data.Migrations
{
    public partial class UtilityNameIsUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Utilities",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Utilities_Name",
                table: "Utilities",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Utilities_Name",
                table: "Utilities");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Utilities",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
