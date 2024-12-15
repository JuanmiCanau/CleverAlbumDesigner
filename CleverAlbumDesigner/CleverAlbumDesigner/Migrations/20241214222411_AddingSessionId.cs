using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleverAlbumDesigner.Migrations
{
    /// <inheritdoc />
    public partial class AddingSessionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Albums");
        }
    }
}
