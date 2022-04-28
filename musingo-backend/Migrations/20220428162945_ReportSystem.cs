using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace musingo_backend.Migrations
{
    public partial class ReportSystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_banned",
                table: "offers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "reports",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    reason = table.Column<int>(type: "int", nullable: false),
                    text = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    reporter_id = table.Column<int>(type: "int", nullable: true),
                    offer_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reports", x => x.id);
                    table.ForeignKey(
                        name: "FK_reports_offers_offer_id",
                        column: x => x.offer_id,
                        principalTable: "offers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_reports_users_reporter_id",
                        column: x => x.reporter_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_reports_offer_id",
                table: "reports",
                column: "offer_id");

            migrationBuilder.CreateIndex(
                name: "IX_reports_reporter_id",
                table: "reports",
                column: "reporter_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reports");

            migrationBuilder.DropColumn(
                name: "is_banned",
                table: "offers");
        }
    }
}
