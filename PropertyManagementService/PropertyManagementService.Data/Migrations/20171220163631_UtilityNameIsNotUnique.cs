using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PropertyManagementService.Data.Migrations
{
    public partial class UtilityNameIsNotUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_BuildingUtilities_Name",
                table: "BuildingUtilities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_BuildingUtilities_Name",
                table: "BuildingUtilities",
                column: "Name");
        }
    }
}
