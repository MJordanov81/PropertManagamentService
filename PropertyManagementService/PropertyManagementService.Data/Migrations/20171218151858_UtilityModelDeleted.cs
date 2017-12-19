using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PropertyManagementService.Data.Migrations
{
    public partial class UtilityModelDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillUtility_Bills_BillId",
                table: "BillUtility");

            migrationBuilder.DropForeignKey(
                name: "FK_BillUtility_BuildingUtilities_UtilityBuildingUtilityId_UtilityBuildingBuildingId",
                table: "BillUtility");

            migrationBuilder.DropForeignKey(
                name: "FK_BuildingUtilities_Buildings_BuildingId",
                table: "BuildingUtilities");

            migrationBuilder.DropForeignKey(
                name: "FK_BuildingUtilities_Utilities_UtilityId",
                table: "BuildingUtilities");

            migrationBuilder.DropForeignKey(
                name: "FK_UnsubscribedUtility_Utilities_UtilityId",
                table: "UnsubscribedUtility");

            migrationBuilder.DropTable(
                name: "Utilities");

            migrationBuilder.DropIndex(
                name: "IX_BillUtility_UtilityBuildingUtilityId_UtilityBuildingBuildingId",
                table: "BillUtility");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BuildingUtilities",
                table: "BuildingUtilities");

            migrationBuilder.DropColumn(
                name: "UtilityBuildingBuildingId",
                table: "BillUtility");

            migrationBuilder.DropColumn(
                name: "UtilityBuildingUtilityId",
                table: "BillUtility");

            migrationBuilder.DropColumn(
                name: "UtilityId",
                table: "BuildingUtilities");

            migrationBuilder.RenameColumn(
                name: "UtilityId",
                table: "UnsubscribedUtility",
                newName: "BuildingUtilityId");

            migrationBuilder.RenameIndex(
                name: "IX_UnsubscribedUtility_UtilityId",
                table: "UnsubscribedUtility",
                newName: "IX_UnsubscribedUtility_BuildingUtilityId");

            migrationBuilder.RenameColumn(
                name: "UtilityBuildingId",
                table: "BillUtility",
                newName: "BuildingUtilityId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BuildingUtilities",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BuildingUtilities",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BuildingUtilities",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BuildingUtilities",
                table: "BuildingUtilities",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_BuildingUtilities_Name",
                table: "BuildingUtilities",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_BillUtility_BuildingUtilityId",
                table: "BillUtility",
                column: "BuildingUtilityId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_BuildingUtilities_Buildings_BuildingId",
                table: "BuildingUtilities",
                column: "BuildingId",
                principalTable: "Buildings",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillUtility_Bills_BillId",
                table: "BillUtility");

            migrationBuilder.DropForeignKey(
                name: "FK_BillUtility_BuildingUtilities_BuildingUtilityId",
                table: "BillUtility");

            migrationBuilder.DropForeignKey(
                name: "FK_BuildingUtilities_Buildings_BuildingId",
                table: "BuildingUtilities");

            migrationBuilder.DropForeignKey(
                name: "FK_UnsubscribedUtility_BuildingUtilities_BuildingUtilityId",
                table: "UnsubscribedUtility");

            migrationBuilder.DropIndex(
                name: "IX_BillUtility_BuildingUtilityId",
                table: "BillUtility");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BuildingUtilities",
                table: "BuildingUtilities");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_BuildingUtilities_Name",
                table: "BuildingUtilities");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BuildingUtilities");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "BuildingUtilities");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BuildingUtilities");

            migrationBuilder.RenameColumn(
                name: "BuildingUtilityId",
                table: "UnsubscribedUtility",
                newName: "UtilityId");

            migrationBuilder.RenameIndex(
                name: "IX_UnsubscribedUtility_BuildingUtilityId",
                table: "UnsubscribedUtility",
                newName: "IX_UnsubscribedUtility_UtilityId");

            migrationBuilder.RenameColumn(
                name: "BuildingUtilityId",
                table: "BillUtility",
                newName: "UtilityBuildingId");

            migrationBuilder.AddColumn<int>(
                name: "UtilityBuildingBuildingId",
                table: "BillUtility",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UtilityBuildingUtilityId",
                table: "BillUtility",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UtilityId",
                table: "BuildingUtilities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BuildingUtilities",
                table: "BuildingUtilities",
                columns: new[] { "UtilityId", "BuildingId" });

            migrationBuilder.CreateTable(
                name: "Utilities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilities", x => x.Id);
                    table.UniqueConstraint("AK_Utilities_Name", x => x.Name);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillUtility_UtilityBuildingUtilityId_UtilityBuildingBuildingId",
                table: "BillUtility",
                columns: new[] { "UtilityBuildingUtilityId", "UtilityBuildingBuildingId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BillUtility_Bills_BillId",
                table: "BillUtility",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillUtility_BuildingUtilities_UtilityBuildingUtilityId_UtilityBuildingBuildingId",
                table: "BillUtility",
                columns: new[] { "UtilityBuildingUtilityId", "UtilityBuildingBuildingId" },
                principalTable: "BuildingUtilities",
                principalColumns: new[] { "UtilityId", "BuildingId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BuildingUtilities_Buildings_BuildingId",
                table: "BuildingUtilities",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BuildingUtilities_Utilities_UtilityId",
                table: "BuildingUtilities",
                column: "UtilityId",
                principalTable: "Utilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnsubscribedUtility_Utilities_UtilityId",
                table: "UnsubscribedUtility",
                column: "UtilityId",
                principalTable: "Utilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
