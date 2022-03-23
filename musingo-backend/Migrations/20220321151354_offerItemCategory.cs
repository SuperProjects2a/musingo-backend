using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace musingo_backend.Migrations
{
    public partial class offerItemCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemCategory",
                table: "offers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemCategory",
                table: "offers");
        }
    }
}
