using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PropertyManagementService.Data.Migrations
{
    public partial class BillModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillUtility_Bills_BillId",
                table: "BillUtility");

            migrationBuilder.DropForeignKey(
                name: "FK_BillUtility_BuildingUtilities_BuildingUtilityId",
                table: "BillUtility");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillUtility",
                table: "BillUtility");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Bills");

            migrationBuilder.RenameTable(
                name: "BillUtility",
                newName: "BillUtilities");

            migrationBuilder.RenameIndex(
                name: "IX_BillUtility_BuildingUtilityId",
                table: "BillUtilities",
                newName: "IX_BillUtilities_BuildingUtilityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillUtilities",
                table: "BillUtilities",
                columns: new[] { "BillId", "BuildingUtilityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BillUtilities_Bills_BillId",
                table: "BillUtilities",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillUtilities_BuildingUtilities_BuildingUtilityId",
                table: "BillUtilities",
                column: "BuildingUtilityId",
                principalTable: "BuildingUtilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillUtilities_Bills_BillId",
                table: "BillUtilities");

            migrationBuilder.DropForeignKey(
                name: "FK_BillUtilities_BuildingUtilities_BuildingUtilityId",
                table: "BillUtilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillUtilities",
                table: "BillUtilities");

            migrationBuilder.RenameTable(
                name: "BillUtilities",
                newName: "BillUtility");

            migrationBuilder.RenameIndex(
                name: "IX_BillUtilities_BuildingUtilityId",
                table: "BillUtility",
                newName: "IX_BillUtility_BuildingUtilityId");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Bills",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillUtility",
                table: "BillUtility",
                columns: new[] { "BillId", "BuildingUtilityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BillUtility_Bills_BillId",
                table: "BillUtility",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillUtility_BuildingUtilities_BuildingUtilityId",
                table: "BillUtility",
                column: "BuildingUtilityId",
                principalTable: "BuildingUtilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
