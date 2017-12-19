using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PropertyManagementService.Data.Migrations
{
    public partial class BillUtilityUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnsubscribedUtility_Apartments_ApartmentId",
                table: "UnsubscribedUtility");

            migrationBuilder.DropForeignKey(
                name: "FK_UnsubscribedUtility_BuildingUtilities_BuildingUtilityId",
                table: "UnsubscribedUtility");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnsubscribedUtility",
                table: "UnsubscribedUtility");

            migrationBuilder.RenameTable(
                name: "UnsubscribedUtility",
                newName: "UnsubscribedUtilities");

            migrationBuilder.RenameIndex(
                name: "IX_UnsubscribedUtility_BuildingUtilityId",
                table: "UnsubscribedUtilities",
                newName: "IX_UnsubscribedUtilities_BuildingUtilityId");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Bills",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnsubscribedUtilities",
                table: "UnsubscribedUtilities",
                columns: new[] { "ApartmentId", "BuildingUtilityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UnsubscribedUtilities_Apartments_ApartmentId",
                table: "UnsubscribedUtilities",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnsubscribedUtilities_BuildingUtilities_BuildingUtilityId",
                table: "UnsubscribedUtilities",
                column: "BuildingUtilityId",
                principalTable: "BuildingUtilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnsubscribedUtilities_Apartments_ApartmentId",
                table: "UnsubscribedUtilities");

            migrationBuilder.DropForeignKey(
                name: "FK_UnsubscribedUtilities_BuildingUtilities_BuildingUtilityId",
                table: "UnsubscribedUtilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnsubscribedUtilities",
                table: "UnsubscribedUtilities");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Bills");

            migrationBuilder.RenameTable(
                name: "UnsubscribedUtilities",
                newName: "UnsubscribedUtility");

            migrationBuilder.RenameIndex(
                name: "IX_UnsubscribedUtilities_BuildingUtilityId",
                table: "UnsubscribedUtility",
                newName: "IX_UnsubscribedUtility_BuildingUtilityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnsubscribedUtility",
                table: "UnsubscribedUtility",
                columns: new[] { "ApartmentId", "BuildingUtilityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UnsubscribedUtility_Apartments_ApartmentId",
                table: "UnsubscribedUtility",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnsubscribedUtility_BuildingUtilities_BuildingUtilityId",
                table: "UnsubscribedUtility",
                column: "BuildingUtilityId",
                principalTable: "BuildingUtilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
