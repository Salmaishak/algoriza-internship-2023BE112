using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Infrastructure.Migrations
{
    public partial class changeBookingDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "discountId",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "finalPrice",
                table: "Booking",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_discountId",
                table: "Booking",
                column: "discountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Discount_discountId",
                table: "Booking",
                column: "discountId",
                principalTable: "Discount",
                principalColumn: "discountID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Discount_discountId",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_discountId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "discountId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "finalPrice",
                table: "Booking");
        }
    }
}
