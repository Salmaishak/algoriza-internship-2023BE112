using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Infrastructure.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "doctorid",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "patientID",
                table: "Booking",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorID",
                table: "Booking",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "doctorID",
                table: "Appointment",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Doctors_doctorID",
                table: "Appointment");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Doctors_DoctorID",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Users_patientID",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_patientID",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "DoctorID",
                table: "Booking",
                newName: "DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_DoctorID",
                table: "Booking",
                newName: "IX_Booking_DoctorId");

            migrationBuilder.RenameColumn(
                name: "doctorID",
                table: "Appointment",
                newName: "doctorId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointment_doctorID",
                table: "Appointment",
                newName: "IX_Appointment_doctorId");

            migrationBuilder.AlterColumn<int>(
                name: "userId",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "doctorid",
                table: "Doctors",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "patientID",
                table: "Booking",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "Booking",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "DoctorID",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PatientId",
                table: "Booking",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "doctorId",
                table: "Appointment",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "doctorID",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_PatientId",
                table: "Booking",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Doctors_doctorId",
                table: "Appointment",
                column: "doctorId",
                principalTable: "Doctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Doctors_DoctorId",
                table: "Booking",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Users_PatientId",
                table: "Booking",
                column: "PatientId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
