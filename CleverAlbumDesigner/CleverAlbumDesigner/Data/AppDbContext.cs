using CleverAlbumDesigner.Models;
using Microsoft.EntityFrameworkCore;

namespace CleverAlbumDesigner.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<PhotoColor> PhotoColor { get; set; }
        public DbSet<ThemeColor> ThemeColor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ThemeColor>()
             .HasOne(tc => tc.Theme)
             .WithMany()
             .HasForeignKey(tc => tc.ThemeId)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ThemeColor>()
                .HasOne(tc => tc.Color)
                .WithMany()
                .HasForeignKey(tc => tc.ColorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Specific Guid for Themes
            var nostalgiaId = Guid.Parse("f25a5b4e-7b63-4c2a-8a14-c97c5b68d034");
            var loveId = Guid.Parse("bb56a798-bcb2-49af-a759-7df8c4f66b31");
            var natureId = Guid.Parse("c4679ee5-299d-4bd5-8dbd-7cb8b6dc2384");
            var adventureId = Guid.Parse("4a39c839-026d-489d-83cf-8b9c8740fcde");

            // Theme insertion
            modelBuilder.Entity<Theme>().HasData(
                new Theme { ThemeId = nostalgiaId, Name = "Nostalgia" },
                new Theme { ThemeId = loveId, Name = "Love" },
                new Theme { ThemeId = natureId, Name = "Nature" },
                new Theme { ThemeId = adventureId, Name = "Adventure" }
            );

            // Specific Guid for Colors
            var sepiaId = Guid.Parse("a47d84a1-b342-4d9e-9e2a-3f89e23a539a");
            var blueGrayId = Guid.Parse("bb74e9cc-e7c8-45aa-8461-3d54e69d86e1");
            var palePinkId = Guid.Parse("af68ec32-2c68-4f84-bc8f-038fda0e6c7e");
            var redId = Guid.Parse("8a28e941-13cd-4946-a9e1-cb5e47b3c236");
            var pinkId = Guid.Parse("d56e73a2-2ad1-450e-80da-df35b6c5e278");
            var purpleId = Guid.Parse("6ac5fa52-0158-4d8a-a13d-e43d5c18a1f6");
            var greenId = Guid.Parse("76ed38b4-35de-4688-9783-dc4d88be53d8");
            var brownId = Guid.Parse("e5d3ac91-5b25-496c-8424-4a4d88b18c56");
            var skyBlueId = Guid.Parse("cd68d4ab-f9b3-44a4-bf4c-fd7183269b9c");
            var orangeId = Guid.Parse("1d5c8f58-d1f6-469e-9c3d-dcf83216b22a");
            var deepBlueId = Guid.Parse("8a24c643-367e-47da-9e2b-5a6b64f73587");
            var reddishBrownId = Guid.Parse("417bdb2b-e534-4925-b7c5-18a7bc89e752");

            // Color insertion
            modelBuilder.Entity<Color>().HasData(
                new Color { ColorId = sepiaId, Name = "Sepia", RgbaCode = "112,66,20" },
                new Color { ColorId = blueGrayId, Name = "Blue-gray", RgbaCode = "96,123,139" },
                new Color { ColorId = palePinkId, Name = "Pale pink", RgbaCode = "250,218,221" },
                new Color { ColorId = redId, Name = "Red", RgbaCode = "255,0,0" },
                new Color { ColorId = pinkId, Name = "Pink", RgbaCode = "255,192,203" },
                new Color { ColorId = purpleId, Name = "Purple", RgbaCode = "128,0,128" },
                new Color { ColorId = greenId, Name = "Green", RgbaCode = "0,128,0" },
                new Color { ColorId = brownId, Name = "Brown", RgbaCode = "139,69,19" },
                new Color { ColorId = skyBlueId, Name = "Sky blue", RgbaCode = "135,206,235" },
                new Color { ColorId = orangeId, Name = "Orange", RgbaCode = "255,165,0" },
                new Color { ColorId = deepBlueId, Name = "Deep blue", RgbaCode = "0,0,139" },
                new Color { ColorId = reddishBrownId, Name = "Reddish-brown", RgbaCode = "153,101,21" }
            );

            var nostalgiaIdColor1 = Guid.Parse("89fb1df2-cb15-4bfc-97f1-98424dcbb02b");
            var nostalgiaIdColor2 = Guid.Parse("a7c91cf3-2a67-4e8e-8f9f-52fdfc70c1a1");
            var nostalgiaIdColor3 = Guid.Parse("6ae94d32-b2fc-4e3d-bc9c-84b279f913a4");
            var loveIdColor1 = Guid.Parse("34be7ad6-9ef0-495c-aedf-40b3f3d54fd3");
            var loveIdColor2 = Guid.Parse("d0585a93-9d81-470d-871f-118baad1db54");
            var loveIdColor3 = Guid.Parse("22b8b4d7-ff5f-4684-87a7-49c4b4398d3c");
            var natureIdColor1 = Guid.Parse("ec8f0b27-0a19-4396-a54a-3f9e2a1da72f");
            var natureIdColor2 = Guid.Parse("3d7fc9b1-d2b3-4b11-9458-55f27f3b52dc");
            var natureIdColor3 = Guid.Parse("5d8c32a5-6cc1-41aa-9d8f-dc3659b3f2ea");
            var adventureIdColor1 = Guid.Parse("f9a7e3b9-8b63-4326-92c6-b13cdbe8579c");
            var adventureIdColor2 = Guid.Parse("a4d732ee-cbd5-47df-98b1-2a65f32b1ac3");
            var adventureIdColor3 = Guid.Parse("0f4edb89-3d2a-46f6-94d9-e11f0a3b9ca2");

            //ThemeColor insertion (junction table)
            modelBuilder.Entity<ThemeColor>().HasData(
                // Nostalgia
                new ThemeColor { ThemeColorId = nostalgiaIdColor1, ThemeId = nostalgiaId, ColorId = sepiaId },
                new ThemeColor { ThemeColorId = nostalgiaIdColor2, ThemeId = nostalgiaId, ColorId = blueGrayId },
                new ThemeColor { ThemeColorId = nostalgiaIdColor3, ThemeId = nostalgiaId, ColorId = palePinkId },

                // Love
                new ThemeColor { ThemeColorId = loveIdColor1, ThemeId = loveId, ColorId = redId },
                new ThemeColor { ThemeColorId = loveIdColor2, ThemeId = loveId, ColorId = pinkId },
                new ThemeColor { ThemeColorId = loveIdColor3, ThemeId = loveId, ColorId = purpleId },

                // Nature
                new ThemeColor { ThemeColorId = natureIdColor1, ThemeId = natureId, ColorId = greenId },
                new ThemeColor { ThemeColorId = natureIdColor2, ThemeId = natureId, ColorId = brownId },
                new ThemeColor { ThemeColorId = natureIdColor3, ThemeId = natureId, ColorId = skyBlueId },

                // Adventure
                new ThemeColor { ThemeColorId = adventureIdColor1, ThemeId = adventureId, ColorId = orangeId },
                new ThemeColor { ThemeColorId = adventureIdColor2, ThemeId = adventureId, ColorId = deepBlueId },
                new ThemeColor { ThemeColorId = adventureIdColor3, ThemeId = adventureId, ColorId = reddishBrownId }
            );
        }

    }
}
