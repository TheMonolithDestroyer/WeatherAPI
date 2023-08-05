using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlobAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Blogs_Header",
                table: "Blogs");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_Header",
                table: "Blogs",
                column: "Header");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Blogs_Header",
                table: "Blogs");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_Header",
                table: "Blogs",
                column: "Header",
                unique: true);
        }
    }
}
