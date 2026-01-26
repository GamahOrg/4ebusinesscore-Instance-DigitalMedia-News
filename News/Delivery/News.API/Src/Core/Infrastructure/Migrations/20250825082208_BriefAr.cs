using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace News.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BriefAr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BriefAr",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BriefAr",
                table: "News");
        }
    }
}
