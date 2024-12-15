using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleverAlbumDesigner.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("1d5c8f58-d1f6-469e-9c3d-dcf83216b22a"),
                column: "RgbaCode",
                value: "255,165,0");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("417bdb2b-e534-4925-b7c5-18a7bc89e752"),
                column: "RgbaCode",
                value: "153,101,21");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("6ac5fa52-0158-4d8a-a13d-e43d5c18a1f6"),
                column: "RgbaCode",
                value: "128,0,128");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("76ed38b4-35de-4688-9783-dc4d88be53d8"),
                column: "RgbaCode",
                value: "0,128,0");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("8a24c643-367e-47da-9e2b-5a6b64f73587"),
                column: "RgbaCode",
                value: "0,0,139");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("8a28e941-13cd-4946-a9e1-cb5e47b3c236"),
                column: "RgbaCode",
                value: "255,0,0");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("a47d84a1-b342-4d9e-9e2a-3f89e23a539a"),
                column: "RgbaCode",
                value: "112,66,20");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("af68ec32-2c68-4f84-bc8f-038fda0e6c7e"),
                column: "RgbaCode",
                value: "250,218,221");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("bb74e9cc-e7c8-45aa-8461-3d54e69d86e1"),
                column: "RgbaCode",
                value: "96,123,139");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("cd68d4ab-f9b3-44a4-bf4c-fd7183269b9c"),
                column: "RgbaCode",
                value: "135,206,235");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("d56e73a2-2ad1-450e-80da-df35b6c5e278"),
                column: "RgbaCode",
                value: "255,192,203");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("e5d3ac91-5b25-496c-8424-4a4d88b18c56"),
                column: "RgbaCode",
                value: "139,69,19");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("1d5c8f58-d1f6-469e-9c3d-dcf83216b22a"),
                column: "RgbaCode",
                value: "255,165,0,255");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("417bdb2b-e534-4925-b7c5-18a7bc89e752"),
                column: "RgbaCode",
                value: "153,101,21,255");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("6ac5fa52-0158-4d8a-a13d-e43d5c18a1f6"),
                column: "RgbaCode",
                value: "128,0,128,255");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("76ed38b4-35de-4688-9783-dc4d88be53d8"),
                column: "RgbaCode",
                value: "0,128,0,255");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("8a24c643-367e-47da-9e2b-5a6b64f73587"),
                column: "RgbaCode",
                value: "0,0,139,255");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("8a28e941-13cd-4946-a9e1-cb5e47b3c236"),
                column: "RgbaCode",
                value: "255,0,0,255");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("a47d84a1-b342-4d9e-9e2a-3f89e23a539a"),
                column: "RgbaCode",
                value: "112,66,20,255");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("af68ec32-2c68-4f84-bc8f-038fda0e6c7e"),
                column: "RgbaCode",
                value: "250,218,221,255");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("bb74e9cc-e7c8-45aa-8461-3d54e69d86e1"),
                column: "RgbaCode",
                value: "96,123,139,255");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("cd68d4ab-f9b3-44a4-bf4c-fd7183269b9c"),
                column: "RgbaCode",
                value: "135,206,235,255");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("d56e73a2-2ad1-450e-80da-df35b6c5e278"),
                column: "RgbaCode",
                value: "255,192,203,255");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "ColorId",
                keyValue: new Guid("e5d3ac91-5b25-496c-8424-4a4d88b18c56"),
                column: "RgbaCode",
                value: "139,69,19,255");
        }
    }
}
