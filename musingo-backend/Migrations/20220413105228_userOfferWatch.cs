using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace musingo_backend.Migrations
{
    public partial class userOfferWatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserOfferWatch",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOfferWatch", x => new { x.OfferId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserOfferWatch_offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "offers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_UserOfferWatch_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserOfferWatch_UserId",
                table: "UserOfferWatch",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserOfferWatch");
        }
    }
}
