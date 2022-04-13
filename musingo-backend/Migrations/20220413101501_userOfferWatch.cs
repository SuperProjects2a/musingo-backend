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
                    WatchedOffersId = table.Column<int>(type: "int", nullable: false),
                    WatchersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_UserOfferWatch_offers_WatchedOffersId",
                        column: x => x.WatchedOffersId,
                        principalTable: "offers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOfferWatch_users_WatchersId",
                        column: x => x.WatchersId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserOfferWatch_WatchedOffersId",
                table: "UserOfferWatch",
                column: "WatchedOffersId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOfferWatch_WatchersId",
                table: "UserOfferWatch",
                column: "WatchersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserOfferWatch");
        }
    }
}
