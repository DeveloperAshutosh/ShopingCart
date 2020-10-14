using Microsoft.EntityFrameworkCore.Migrations;

namespace AshZoneModels.Migrations
{
    public partial class Added_new_tables_for_cart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrderHeaders");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "OrderHeaders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "OrderHeaders");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
