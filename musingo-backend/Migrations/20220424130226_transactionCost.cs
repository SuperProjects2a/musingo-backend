using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace musingo_backend.Migrations
{
    public partial class transactionCost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WalletBalance",
                table: "users",
                newName: "wallet_balance");

            migrationBuilder.AddColumn<double>(
                name: "cost",
                table: "transactions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cost",
                table: "transactions");

            migrationBuilder.RenameColumn(
                name: "wallet_balance",
                table: "users",
                newName: "WalletBalance");
        }
    }
}
