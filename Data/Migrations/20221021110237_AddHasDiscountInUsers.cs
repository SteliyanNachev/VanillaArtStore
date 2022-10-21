using Microsoft.EntityFrameworkCore.Migrations;

namespace VanillaArtStore.Data.Migrations
{
    public partial class AddHasDiscountInUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasDiscount",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasDiscount",
                table: "AspNetUsers");
        }
    }
}
