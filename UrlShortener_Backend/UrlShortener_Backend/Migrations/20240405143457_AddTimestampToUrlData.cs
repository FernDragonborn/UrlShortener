using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddTimestampToUrlData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Version",
                table: "Urls",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "Urls");
        }
    }
}
