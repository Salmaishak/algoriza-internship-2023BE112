using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Infrastructure.Migrations
{
    public partial class BookingandDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.RenameColumn(
                name: "discountId",
                table: "Booking",
                newName: "DiscountId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_discountId",
                table: "Booking",
                newName: "IX_Booking_DiscountId");

            migrationBuilder.AlterColumn<int>(
                name: "DiscountId",
                table: "Booking",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Discount_DiscountId",
                table: "Booking",
                column: "DiscountId",
                principalTable: "Discount",
                principalColumn: "discountID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Discount_DiscountId",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "DiscountId",
                table: "Booking",
                newName: "discountId");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_DiscountId",
                table: "Booking",
                newName: "IX_Booking_discountId");

            migrationBuilder.AlterColumn<int>(
                name: "discountId",
                table: "Booking",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Discount_DiscountId_Rollback",
                table: "Booking",
                column: "discountId",
                principalTable: "Discount",
                principalColumn: "discountID",
                onDelete: ReferentialAction.Restrict);
        }

    }
}
