using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingSystem.Data.Migrations
{
    public partial class AddedRoomToBookEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_GuestHouses_GuestHouseId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "GuestHouseId",
                table: "Bookings",
                newName: "RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_GuestHouseId",
                table: "Bookings",
                newName: "IX_Bookings_RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Rooms_RoomId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "Bookings",
                newName: "GuestHouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings",
                newName: "IX_Bookings_GuestHouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_GuestHouses_GuestHouseId",
                table: "Bookings",
                column: "GuestHouseId",
                principalTable: "GuestHouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
