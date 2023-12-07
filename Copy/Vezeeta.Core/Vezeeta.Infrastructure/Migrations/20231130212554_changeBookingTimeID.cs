using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Infrastructure.Migrations
{
    public partial class changeBookingTimeID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Appointment_appointmentID",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "appointmentID",
                table: "Booking",
                newName: "timeSlotID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_appointmentID",
                table: "Booking",
                newName: "IX_Booking_timeSlotID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_TimeSlot_timeSlotID",
                table: "Booking",
                column: "timeSlotID",
                principalTable: "TimeSlot",
                principalColumn: "SlotId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_TimeSlot_timeSlotID",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "timeSlotID",
                table: "Booking",
                newName: "appointmentID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_timeSlotID",
                table: "Booking",
                newName: "IX_Booking_appointmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Appointment_appointmentID",
                table: "Booking",
                column: "appointmentID",
                principalTable: "Appointment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
