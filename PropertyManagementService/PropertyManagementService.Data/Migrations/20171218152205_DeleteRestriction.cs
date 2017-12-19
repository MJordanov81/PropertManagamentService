using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PropertyManagementService.Data.Migrations
{
    public partial class DeleteRestriction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnsubscribedUtility_Apartments_ApartmentId",
                table: "UnsubscribedUtility");

            migrationBuilder.AddForeignKey(
                name: "FK_UnsubscribedUtility_Apartments_ApartmentId",
                table: "UnsubscribedUtility",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnsubscribedUtility_Apartments_ApartmentId",
                table: "UnsubscribedUtility");

            migrationBuilder.AddForeignKey(
                name: "FK_UnsubscribedUtility_Apartments_ApartmentId",
                table: "UnsubscribedUtility",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
