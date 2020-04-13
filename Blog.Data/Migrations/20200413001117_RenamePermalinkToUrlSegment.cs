using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.Data.Migrations
{
    public partial class RenamePermalinkToUrlSegment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_Permalink",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Permalink",
                table: "Posts",
                newName: "UrlSegment");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UrlSegment",
                table: "Posts",
                column: "UrlSegment",
                unique: true,
                filter: "[UrlSegment] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_UrlSegment",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "UrlSegment",
                table: "Posts",
                newName: "Permalink");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Permalink",
                table: "Posts",
                column: "Permalink",
                unique: true,
                filter: "[Permalink] IS NOT NULL");
        }
    }
}
