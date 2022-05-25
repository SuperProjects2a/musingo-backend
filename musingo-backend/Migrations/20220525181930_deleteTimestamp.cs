using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace musingo_backend.Migrations
{
    public partial class deleteTimestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_update_time",
                table: "transactions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "last_update_time",
                table: "transactions",
                type: "timestamp",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
