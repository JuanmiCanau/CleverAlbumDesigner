using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CleverAlbumDesigner.Migrations
{
    /// <inheritdoc />
    public partial class FixingErrorsAndRecreating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Theme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.AlbumId);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    ColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RgbaCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.ColorId);
                });

            migrationBuilder.CreateTable(
                name: "PhotoColor",
                columns: table => new
                {
                    PhotoColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoColor", x => x.PhotoColorId);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    PhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DominantColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.PhotoId);
                });

            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    ThemeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.ThemeId);
                });

            migrationBuilder.CreateTable(
                name: "ThemeColor",
                columns: table => new
                {
                    ThemeColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThemeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThemeColor", x => x.ThemeColorId);
                    table.ForeignKey(
                        name: "FK_ThemeColor_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "ColorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ThemeColor_Themes_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Themes",
                        principalColumn: "ThemeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "ColorId", "Name", "RgbaCode" },
                values: new object[,]
                {
                    { new Guid("1d5c8f58-d1f6-469e-9c3d-dcf83216b22a"), "Orange", "255,165,0,255" },
                    { new Guid("417bdb2b-e534-4925-b7c5-18a7bc89e752"), "Reddish-brown", "153,101,21,255" },
                    { new Guid("6ac5fa52-0158-4d8a-a13d-e43d5c18a1f6"), "Purple", "128,0,128,255" },
                    { new Guid("76ed38b4-35de-4688-9783-dc4d88be53d8"), "Green", "0,128,0,255" },
                    { new Guid("8a24c643-367e-47da-9e2b-5a6b64f73587"), "Deep blue", "0,0,139,255" },
                    { new Guid("8a28e941-13cd-4946-a9e1-cb5e47b3c236"), "Red", "255,0,0,255" },
                    { new Guid("a47d84a1-b342-4d9e-9e2a-3f89e23a539a"), "Sepia", "112,66,20,255" },
                    { new Guid("af68ec32-2c68-4f84-bc8f-038fda0e6c7e"), "Pale pink", "250,218,221,255" },
                    { new Guid("bb74e9cc-e7c8-45aa-8461-3d54e69d86e1"), "Blue-gray", "96,123,139,255" },
                    { new Guid("cd68d4ab-f9b3-44a4-bf4c-fd7183269b9c"), "Sky blue", "135,206,235,255" },
                    { new Guid("d56e73a2-2ad1-450e-80da-df35b6c5e278"), "Pink", "255,192,203,255" },
                    { new Guid("e5d3ac91-5b25-496c-8424-4a4d88b18c56"), "Brown", "139,69,19,255" }
                });

            migrationBuilder.InsertData(
                table: "Themes",
                columns: new[] { "ThemeId", "Name" },
                values: new object[,]
                {
                    { new Guid("4a39c839-026d-489d-83cf-8b9c8740fcde"), "Adventure" },
                    { new Guid("bb56a798-bcb2-49af-a759-7df8c4f66b31"), "Love" },
                    { new Guid("c4679ee5-299d-4bd5-8dbd-7cb8b6dc2384"), "Nature" },
                    { new Guid("f25a5b4e-7b63-4c2a-8a14-c97c5b68d034"), "Nostalgia" }
                });

            migrationBuilder.InsertData(
                table: "ThemeColor",
                columns: new[] { "ThemeColorId", "ColorId", "ThemeId" },
                values: new object[,]
                {
                    { new Guid("0f4edb89-3d2a-46f6-94d9-e11f0a3b9ca2"), new Guid("417bdb2b-e534-4925-b7c5-18a7bc89e752"), new Guid("4a39c839-026d-489d-83cf-8b9c8740fcde") },
                    { new Guid("22b8b4d7-ff5f-4684-87a7-49c4b4398d3c"), new Guid("6ac5fa52-0158-4d8a-a13d-e43d5c18a1f6"), new Guid("bb56a798-bcb2-49af-a759-7df8c4f66b31") },
                    { new Guid("34be7ad6-9ef0-495c-aedf-40b3f3d54fd3"), new Guid("8a28e941-13cd-4946-a9e1-cb5e47b3c236"), new Guid("bb56a798-bcb2-49af-a759-7df8c4f66b31") },
                    { new Guid("3d7fc9b1-d2b3-4b11-9458-55f27f3b52dc"), new Guid("e5d3ac91-5b25-496c-8424-4a4d88b18c56"), new Guid("c4679ee5-299d-4bd5-8dbd-7cb8b6dc2384") },
                    { new Guid("5d8c32a5-6cc1-41aa-9d8f-dc3659b3f2ea"), new Guid("cd68d4ab-f9b3-44a4-bf4c-fd7183269b9c"), new Guid("c4679ee5-299d-4bd5-8dbd-7cb8b6dc2384") },
                    { new Guid("6ae94d32-b2fc-4e3d-bc9c-84b279f913a4"), new Guid("af68ec32-2c68-4f84-bc8f-038fda0e6c7e"), new Guid("f25a5b4e-7b63-4c2a-8a14-c97c5b68d034") },
                    { new Guid("89fb1df2-cb15-4bfc-97f1-98424dcbb02b"), new Guid("a47d84a1-b342-4d9e-9e2a-3f89e23a539a"), new Guid("f25a5b4e-7b63-4c2a-8a14-c97c5b68d034") },
                    { new Guid("a4d732ee-cbd5-47df-98b1-2a65f32b1ac3"), new Guid("8a24c643-367e-47da-9e2b-5a6b64f73587"), new Guid("4a39c839-026d-489d-83cf-8b9c8740fcde") },
                    { new Guid("a7c91cf3-2a67-4e8e-8f9f-52fdfc70c1a1"), new Guid("bb74e9cc-e7c8-45aa-8461-3d54e69d86e1"), new Guid("f25a5b4e-7b63-4c2a-8a14-c97c5b68d034") },
                    { new Guid("d0585a93-9d81-470d-871f-118baad1db54"), new Guid("d56e73a2-2ad1-450e-80da-df35b6c5e278"), new Guid("bb56a798-bcb2-49af-a759-7df8c4f66b31") },
                    { new Guid("ec8f0b27-0a19-4396-a54a-3f9e2a1da72f"), new Guid("76ed38b4-35de-4688-9783-dc4d88be53d8"), new Guid("c4679ee5-299d-4bd5-8dbd-7cb8b6dc2384") },
                    { new Guid("f9a7e3b9-8b63-4326-92c6-b13cdbe8579c"), new Guid("1d5c8f58-d1f6-469e-9c3d-dcf83216b22a"), new Guid("4a39c839-026d-489d-83cf-8b9c8740fcde") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThemeColor_ColorId",
                table: "ThemeColor",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_ThemeColor_ThemeId",
                table: "ThemeColor",
                column: "ThemeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "PhotoColor");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "ThemeColor");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Themes");
        }
    }
}
