using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleverAlbumDesigner.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "Theme",
                table: "Albums");

            migrationBuilder.AddColumn<Guid>(
                name: "ThemeId",
                table: "Albums",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Albums_ThemeId",
                table: "Albums",
                column: "ThemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Themes_ThemeId",
                table: "Albums",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "ThemeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Themes_ThemeId",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Albums_ThemeId",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "ThemeId",
                table: "Albums");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Theme",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
