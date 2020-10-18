using Microsoft.EntityFrameworkCore.Migrations;

namespace AshZoneModels.Migrations
{
    public partial class updatedtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "OrderHeaders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
