using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vezeeta.Infrastructure.Migrations
{
    public partial class changeDiscountActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "discountActivity",
                table: "Discounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "discountActivity",
                table: "Discounts");
        }
    }
}
