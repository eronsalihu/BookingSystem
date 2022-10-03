using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingSystem.Data.Migrations
{
    public partial class ModifiedGuestHousesAndRooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_GuestHouses_GuestHouseId",
                table: "Rooms");

            migrationBuilder.AlterColumn<int>(
                name: "GuestHouseId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_GuestHouses_GuestHouseId",
                table: "Rooms",
                column: "GuestHouseId",
                principalTable: "GuestHouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_GuestHouses_GuestHouseId",
                table: "Rooms");

            migrationBuilder.AlterColumn<int>(
                name: "GuestHouseId",
                table: "Rooms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_GuestHouses_GuestHouseId",
                table: "Rooms",
                column: "GuestHouseId",
                principalTable: "GuestHouses",
                principalColumn: "Id");
        }
    }
}
