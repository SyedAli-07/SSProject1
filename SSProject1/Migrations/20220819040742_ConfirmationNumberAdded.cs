using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SSProject1.Migrations
{
    public partial class ConfirmationNumberAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConfirmationNumber",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmationNumber",
                table: "Booking");
        }
    }
}
