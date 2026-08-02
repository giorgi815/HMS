using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationRooms_Rooms_RoomId1",
                table: "ReservationRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Reservations_ReservationId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_ReservationId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_ReservationRooms_RoomId1",
                table: "ReservationRooms");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "ReservationRoomId",
                table: "ReservationRooms");

            migrationBuilder.DropColumn(
                name: "RoomId1",
                table: "ReservationRooms");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Rooms",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Rooms",
                type: "int",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReservationRoomId",
                table: "ReservationRooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoomId1",
                table: "ReservationRooms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_ReservationId",
                table: "Rooms",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationRooms_RoomId1",
                table: "ReservationRooms",
                column: "RoomId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationRooms_Rooms_RoomId1",
                table: "ReservationRooms",
                column: "RoomId1",
                principalTable: "Rooms",
                principalColumn: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Reservations_ReservationId",
                table: "Rooms",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "ReservationId");
        }
    }
}
