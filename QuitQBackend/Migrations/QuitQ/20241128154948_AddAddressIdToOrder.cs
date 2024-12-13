using Microsoft.EntityFrameworkCore.Migrations;

namespace QuitQBackend.Migrations.QuitQ {
    public partial class AddAddressIdToOrder : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            // Add the AddressId column to the Orders table
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Orders",
                type: "int",
                nullable: true);

            // Create a foreign key relationship between Orders and DeliveryAddress tables
            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryAddress_AddressId",
                table: "Orders",
                column: "AddressId",
                principalTable: "DeliveryAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            // Recreate the index for AddressId (optional, based on your previous migrations)
            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressId",
                table: "Orders",
                column: "AddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            // Drop the foreign key and the AddressId column
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryAddress_AddressId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AddressId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Orders");
        }
    }
}
