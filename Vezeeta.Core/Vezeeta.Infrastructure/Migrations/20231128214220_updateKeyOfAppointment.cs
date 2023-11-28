using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Infrastructure.Migrations
{
    public partial class updateKeyOfAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "timeID",
                table: "Appointments",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                columns: new[] { "timeID", "dayOfWeek" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments");

            migrationBuilder.AlterColumn<DateTime>(
                name: "timeID",
                table: "Appointments",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Appointments",
                table: "Appointments",
                column: "timeID");
        }
    }
}
