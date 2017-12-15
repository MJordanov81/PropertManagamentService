namespace PropertyManagementService.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class UserTableChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartment_Building_BuildingId",
                table: "Apartment");

            migrationBuilder.DropForeignKey(
                name: "FK_Apartment_AspNetUsers_OwnerId",
                table: "Apartment");

            migrationBuilder.DropForeignKey(
                name: "FK_Bill_Apartment_ApartmentId",
                table: "Bill");

            migrationBuilder.DropForeignKey(
                name: "FK_BillUtility_Bill_BillId",
                table: "BillUtility");

            migrationBuilder.DropForeignKey(
                name: "FK_BillUtility_UtilityBuilding_UtilityBuildingUtilityId_UtilityBuildingBuildingId",
                table: "BillUtility");

            migrationBuilder.DropForeignKey(
                name: "FK_Building_AspNetUsers_ManagerId",
                table: "Building");

            migrationBuilder.DropForeignKey(
                name: "FK_UnsubscribedUtility_Apartment_ApartmentId",
                table: "UnsubscribedUtility");

            migrationBuilder.DropForeignKey(
                name: "FK_UnsubscribedUtility_Utility_UtilityId",
                table: "UnsubscribedUtility");

            migrationBuilder.DropForeignKey(
                name: "FK_UtilityBuilding_Building_BuildingId",
                table: "UtilityBuilding");

            migrationBuilder.DropForeignKey(
                name: "FK_UtilityBuilding_Utility_UtilityId",
                table: "UtilityBuilding");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UtilityBuilding",
                table: "UtilityBuilding");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Utility",
                table: "Utility");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Building",
                table: "Building");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bill",
                table: "Bill");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Apartment",
                table: "Apartment");

            migrationBuilder.RenameTable(
                name: "UtilityBuilding",
                newName: "BuildingUtilities");

            migrationBuilder.RenameTable(
                name: "Utility",
                newName: "Utilities");

            migrationBuilder.RenameTable(
                name: "Building",
                newName: "Buildings");

            migrationBuilder.RenameTable(
                name: "Bill",
                newName: "Bills");

            migrationBuilder.RenameTable(
                name: "Apartment",
                newName: "Apartments");

            migrationBuilder.RenameIndex(
                name: "IX_UtilityBuilding_BuildingId",
                table: "BuildingUtilities",
                newName: "IX_BuildingUtilities_BuildingId");

            migrationBuilder.RenameIndex(
                name: "IX_Building_ManagerId",
                table: "Buildings",
                newName: "IX_Buildings_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Bill_ApartmentId",
                table: "Bills",
                newName: "IX_Bills_ApartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Apartment_OwnerId",
                table: "Apartments",
                newName: "IX_Apartments_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Apartment_BuildingId",
                table: "Apartments",
                newName: "IX_Apartments_BuildingId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Utilities",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Utilities",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Buildings",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 250);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BuildingUtilities",
                table: "BuildingUtilities",
                columns: new[] { "UtilityId", "BuildingId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Utilities",
                table: "Utilities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Buildings",
                table: "Buildings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bills",
                table: "Bills",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Apartments",
                table: "Apartments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Buildings_BuildingId",
                table: "Apartments",
                column: "BuildingId",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_AspNetUsers_OwnerId",
                table: "Apartments",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Apartments_ApartmentId",
                table: "Bills",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Buildings_AspNetUsers_ManagerId",
                table: "Buildings",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
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
                name: "FK_UnsubscribedUtility_Apartments_ApartmentId",
                table: "UnsubscribedUtility",
                column: "ApartmentId",
                principalTable: "Apartments",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Buildings_BuildingId",
                table: "Apartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_AspNetUsers_OwnerId",
                table: "Apartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Apartments_ApartmentId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_BillUtility_Bills_BillId",
                table: "BillUtility");

            migrationBuilder.DropForeignKey(
                name: "FK_BillUtility_BuildingUtilities_UtilityBuildingUtilityId_UtilityBuildingBuildingId",
                table: "BillUtility");

            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_AspNetUsers_ManagerId",
                table: "Buildings");

            migrationBuilder.DropForeignKey(
                name: "FK_BuildingUtilities_Buildings_BuildingId",
                table: "BuildingUtilities");

            migrationBuilder.DropForeignKey(
                name: "FK_BuildingUtilities_Utilities_UtilityId",
                table: "BuildingUtilities");

            migrationBuilder.DropForeignKey(
                name: "FK_UnsubscribedUtility_Apartments_ApartmentId",
                table: "UnsubscribedUtility");

            migrationBuilder.DropForeignKey(
                name: "FK_UnsubscribedUtility_Utilities_UtilityId",
                table: "UnsubscribedUtility");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Utilities",
                table: "Utilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BuildingUtilities",
                table: "BuildingUtilities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Buildings",
                table: "Buildings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bills",
                table: "Bills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Apartments",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "Utilities",
                newName: "Utility");

            migrationBuilder.RenameTable(
                name: "BuildingUtilities",
                newName: "UtilityBuilding");

            migrationBuilder.RenameTable(
                name: "Buildings",
                newName: "Building");

            migrationBuilder.RenameTable(
                name: "Bills",
                newName: "Bill");

            migrationBuilder.RenameTable(
                name: "Apartments",
                newName: "Apartment");

            migrationBuilder.RenameIndex(
                name: "IX_BuildingUtilities_BuildingId",
                table: "UtilityBuilding",
                newName: "IX_UtilityBuilding_BuildingId");

            migrationBuilder.RenameIndex(
                name: "IX_Buildings_ManagerId",
                table: "Building",
                newName: "IX_Building_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_ApartmentId",
                table: "Bill",
                newName: "IX_Bill_ApartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Apartments_OwnerId",
                table: "Apartment",
                newName: "IX_Apartment_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Apartments_BuildingId",
                table: "Apartment",
                newName: "IX_Apartment_BuildingId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Utility",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Utility",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Building",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Utility",
                table: "Utility",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UtilityBuilding",
                table: "UtilityBuilding",
                columns: new[] { "UtilityId", "BuildingId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Building",
                table: "Building",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bill",
                table: "Bill",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Apartment",
                table: "Apartment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartment_Building_BuildingId",
                table: "Apartment",
                column: "BuildingId",
                principalTable: "Building",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Apartment_AspNetUsers_OwnerId",
                table: "Apartment",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_Apartment_ApartmentId",
                table: "Bill",
                column: "ApartmentId",
                principalTable: "Apartment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillUtility_Bill_BillId",
                table: "BillUtility",
                column: "BillId",
                principalTable: "Bill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillUtility_UtilityBuilding_UtilityBuildingUtilityId_UtilityBuildingBuildingId",
                table: "BillUtility",
                columns: new[] { "UtilityBuildingUtilityId", "UtilityBuildingBuildingId" },
                principalTable: "UtilityBuilding",
                principalColumns: new[] { "UtilityId", "BuildingId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Building_AspNetUsers_ManagerId",
                table: "Building",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnsubscribedUtility_Apartment_ApartmentId",
                table: "UnsubscribedUtility",
                column: "ApartmentId",
                principalTable: "Apartment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnsubscribedUtility_Utility_UtilityId",
                table: "UnsubscribedUtility",
                column: "UtilityId",
                principalTable: "Utility",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UtilityBuilding_Building_BuildingId",
                table: "UtilityBuilding",
                column: "BuildingId",
                principalTable: "Building",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UtilityBuilding_Utility_UtilityId",
                table: "UtilityBuilding",
                column: "UtilityId",
                principalTable: "Utility",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
