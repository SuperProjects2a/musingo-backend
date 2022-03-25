using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace musingo_backend.Migrations
{
    public partial class UserComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "user_comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_comments_user_id",
                table: "user_comments",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_comments_users_user_id",
                table: "user_comments",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_comments_users_user_id",
                table: "user_comments");

            migrationBuilder.DropIndex(
                name: "IX_user_comments_user_id",
                table: "user_comments");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "user_comments");
        }
    }
}
