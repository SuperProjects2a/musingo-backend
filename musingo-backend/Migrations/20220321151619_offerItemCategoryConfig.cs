using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace musingo_backend.Migrations
{
    public partial class offerItemCategoryConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemCategory",
                table: "offers",
                newName: "item_category");

            migrationBuilder.AlterColumn<string>(
                name: "item_category",
                table: "offers",
                type: "nvarchar(30)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "item_category",
                table: "offers",
                newName: "ItemCategory");

            migrationBuilder.AlterColumn<int>(
                name: "ItemCategory",
                table: "offers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)");
        }
    }
}
