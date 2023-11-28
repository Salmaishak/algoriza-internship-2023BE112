using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Infrastructure.Migrations
{
    public partial class initialMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    discountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    discountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    discountType = table.Column<int>(type: "int", nullable: false),
                    numOfRequests = table.Column<int>(type: "int", nullable: false),
                    valueOfDiscount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.discountID);
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    specializationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    specializationName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.specializationID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<int>(type: "int", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    specializationID = table.Column<int>(type: "int", nullable: true),
                    PatientuserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                    table.ForeignKey(
                        name: "FK_Users_Specializations_specializationID",
                        column: x => x.specializationID,
                        principalTable: "Specializations",
                        principalColumn: "specializationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Users_PatientuserId",
                        column: x => x.PatientuserId,
                        principalTable: "Users",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    timeID = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dayOfWeek = table.Column<int>(type: "int", nullable: false),
                    discountID = table.Column<int>(type: "int", nullable: false),
                    doctorID = table.Column<int>(type: "int", nullable: false),
                    patientID = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<float>(type: "real", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.timeID);
                    table.ForeignKey(
                        name: "FK_Appointments_Discounts_discountID",
                        column: x => x.discountID,
                        principalTable: "Discounts",
                        principalColumn: "discountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Users_doctorID",
                        column: x => x.doctorID,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Appointments_Users_patientID",
                        column: x => x.patientID,
                        principalTable: "Users",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_discountID",
                table: "Appointments",
                column: "discountID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_doctorID",
                table: "Appointments",
                column: "doctorID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_patientID",
                table: "Appointments",
                column: "patientID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PatientuserId",
                table: "Users",
                column: "PatientuserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_specializationID",
                table: "Users",
                column: "specializationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Specializations");
        }
    }
}
