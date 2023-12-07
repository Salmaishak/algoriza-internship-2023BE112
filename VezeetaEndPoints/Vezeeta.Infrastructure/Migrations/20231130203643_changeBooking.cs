using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Infrastructure.Migrations
{
    public partial class changeBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "time",
                table: "Booking");

            migrationBuilder.AddColumn<int>(
                name: "appointmentID",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_appointmentID",
                table: "Booking",
                column: "appointmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Appointment_appointmentID",
                table: "Booking",
                column: "appointmentID",
                principalTable: "Appointment",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Appointment_appointmentID",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_appointmentID",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "appointmentID",
                table: "Booking");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "time",
                table: "Booking",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
