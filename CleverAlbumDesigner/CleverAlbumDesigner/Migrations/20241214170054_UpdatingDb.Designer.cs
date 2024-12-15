﻿// <auto-generated />
using System;
using CleverAlbumDesigner.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CleverAlbumDesigner.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241214170054_UpdatingDb")]
    partial class UpdatingDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CleverAlbumDesigner.Models.Album", b =>
                {
                    b.Property<Guid>("AlbumId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ThemeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AlbumId");

                    b.HasIndex("ThemeId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("CleverAlbumDesigner.Models.Color", b =>
                {
                    b.Property<Guid>("ColorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RgbaCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ColorId");

                    b.ToTable("Colors");

                    b.HasData(
                        new
                        {
                            ColorId = new Guid("a47d84a1-b342-4d9e-9e2a-3f89e23a539a"),
                            Name = "Sepia",
                            RgbaCode = "112,66,20,255"
                        },
                        new
                        {
                            ColorId = new Guid("bb74e9cc-e7c8-45aa-8461-3d54e69d86e1"),
                            Name = "Blue-gray",
                            RgbaCode = "96,123,139,255"
                        },
                        new
                        {
                            ColorId = new Guid("af68ec32-2c68-4f84-bc8f-038fda0e6c7e"),
                            Name = "Pale pink",
                            RgbaCode = "250,218,221,255"
                        },
                        new
                        {
                            ColorId = new Guid("8a28e941-13cd-4946-a9e1-cb5e47b3c236"),
                            Name = "Red",
                            RgbaCode = "255,0,0,255"
                        },
                        new
                        {
                            ColorId = new Guid("d56e73a2-2ad1-450e-80da-df35b6c5e278"),
                            Name = "Pink",
                            RgbaCode = "255,192,203,255"
                        },
                        new
                        {
                            ColorId = new Guid("6ac5fa52-0158-4d8a-a13d-e43d5c18a1f6"),
                            Name = "Purple",
                            RgbaCode = "128,0,128,255"
                        },
                        new
                        {
                            ColorId = new Guid("76ed38b4-35de-4688-9783-dc4d88be53d8"),
                            Name = "Green",
                            RgbaCode = "0,128,0,255"
                        },
                        new
                        {
                            ColorId = new Guid("e5d3ac91-5b25-496c-8424-4a4d88b18c56"),
                            Name = "Brown",
                            RgbaCode = "139,69,19,255"
                        },
                        new
                        {
                            ColorId = new Guid("cd68d4ab-f9b3-44a4-bf4c-fd7183269b9c"),
                            Name = "Sky blue",
                            RgbaCode = "135,206,235,255"
                        },
                        new
                        {
                            ColorId = new Guid("1d5c8f58-d1f6-469e-9c3d-dcf83216b22a"),
                            Name = "Orange",
                            RgbaCode = "255,165,0,255"
                        },
                        new
                        {
                            ColorId = new Guid("8a24c643-367e-47da-9e2b-5a6b64f73587"),
                            Name = "Deep blue",
                            RgbaCode = "0,0,139,255"
                        },
                        new
                        {
                            ColorId = new Guid("417bdb2b-e534-4925-b7c5-18a7bc89e752"),
                            Name = "Reddish-brown",
                            RgbaCode = "153,101,21,255"
                        });
                });

            modelBuilder.Entity("CleverAlbumDesigner.Models.Photo", b =>
                {
                    b.Property<Guid>("PhotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AlbumId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DominantColorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UploadedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PhotoId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("CleverAlbumDesigner.Models.PhotoColor", b =>
                {
                    b.Property<Guid>("PhotoColorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ColorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PhotoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PhotoColorId");

                    b.ToTable("PhotoColor");
                });

            modelBuilder.Entity("CleverAlbumDesigner.Models.Theme", b =>
                {
                    b.Property<Guid>("ThemeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ThemeId");

                    b.ToTable("Themes");

                    b.HasData(
                        new
                        {
                            ThemeId = new Guid("f25a5b4e-7b63-4c2a-8a14-c97c5b68d034"),
                            Name = "Nostalgia"
                        },
                        new
                        {
                            ThemeId = new Guid("bb56a798-bcb2-49af-a759-7df8c4f66b31"),
                            Name = "Love"
                        },
                        new
                        {
                            ThemeId = new Guid("c4679ee5-299d-4bd5-8dbd-7cb8b6dc2384"),
                            Name = "Nature"
                        },
                        new
                        {
                            ThemeId = new Guid("4a39c839-026d-489d-83cf-8b9c8740fcde"),
                            Name = "Adventure"
                        });
                });

            modelBuilder.Entity("CleverAlbumDesigner.Models.ThemeColor", b =>
                {
                    b.Property<Guid>("ThemeColorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ColorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ThemeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ThemeColorId");

                    b.HasIndex("ColorId");

                    b.HasIndex("ThemeId");

                    b.ToTable("ThemeColor");

                    b.HasData(
                        new
                        {
                            ThemeColorId = new Guid("89fb1df2-cb15-4bfc-97f1-98424dcbb02b"),
                            ColorId = new Guid("a47d84a1-b342-4d9e-9e2a-3f89e23a539a"),
                            ThemeId = new Guid("f25a5b4e-7b63-4c2a-8a14-c97c5b68d034")
                        },
                        new
                        {
                            ThemeColorId = new Guid("a7c91cf3-2a67-4e8e-8f9f-52fdfc70c1a1"),
                            ColorId = new Guid("bb74e9cc-e7c8-45aa-8461-3d54e69d86e1"),
                            ThemeId = new Guid("f25a5b4e-7b63-4c2a-8a14-c97c5b68d034")
                        },
                        new
                        {
                            ThemeColorId = new Guid("6ae94d32-b2fc-4e3d-bc9c-84b279f913a4"),
                            ColorId = new Guid("af68ec32-2c68-4f84-bc8f-038fda0e6c7e"),
                            ThemeId = new Guid("f25a5b4e-7b63-4c2a-8a14-c97c5b68d034")
                        },
                        new
                        {
                            ThemeColorId = new Guid("34be7ad6-9ef0-495c-aedf-40b3f3d54fd3"),
                            ColorId = new Guid("8a28e941-13cd-4946-a9e1-cb5e47b3c236"),
                            ThemeId = new Guid("bb56a798-bcb2-49af-a759-7df8c4f66b31")
                        },
                        new
                        {
                            ThemeColorId = new Guid("d0585a93-9d81-470d-871f-118baad1db54"),
                            ColorId = new Guid("d56e73a2-2ad1-450e-80da-df35b6c5e278"),
                            ThemeId = new Guid("bb56a798-bcb2-49af-a759-7df8c4f66b31")
                        },
                        new
                        {
                            ThemeColorId = new Guid("22b8b4d7-ff5f-4684-87a7-49c4b4398d3c"),
                            ColorId = new Guid("6ac5fa52-0158-4d8a-a13d-e43d5c18a1f6"),
                            ThemeId = new Guid("bb56a798-bcb2-49af-a759-7df8c4f66b31")
                        },
                        new
                        {
                            ThemeColorId = new Guid("ec8f0b27-0a19-4396-a54a-3f9e2a1da72f"),
                            ColorId = new Guid("76ed38b4-35de-4688-9783-dc4d88be53d8"),
                            ThemeId = new Guid("c4679ee5-299d-4bd5-8dbd-7cb8b6dc2384")
                        },
                        new
                        {
                            ThemeColorId = new Guid("3d7fc9b1-d2b3-4b11-9458-55f27f3b52dc"),
                            ColorId = new Guid("e5d3ac91-5b25-496c-8424-4a4d88b18c56"),
                            ThemeId = new Guid("c4679ee5-299d-4bd5-8dbd-7cb8b6dc2384")
                        },
                        new
                        {
                            ThemeColorId = new Guid("5d8c32a5-6cc1-41aa-9d8f-dc3659b3f2ea"),
                            ColorId = new Guid("cd68d4ab-f9b3-44a4-bf4c-fd7183269b9c"),
                            ThemeId = new Guid("c4679ee5-299d-4bd5-8dbd-7cb8b6dc2384")
                        },
                        new
                        {
                            ThemeColorId = new Guid("f9a7e3b9-8b63-4326-92c6-b13cdbe8579c"),
                            ColorId = new Guid("1d5c8f58-d1f6-469e-9c3d-dcf83216b22a"),
                            ThemeId = new Guid("4a39c839-026d-489d-83cf-8b9c8740fcde")
                        },
                        new
                        {
                            ThemeColorId = new Guid("a4d732ee-cbd5-47df-98b1-2a65f32b1ac3"),
                            ColorId = new Guid("8a24c643-367e-47da-9e2b-5a6b64f73587"),
                            ThemeId = new Guid("4a39c839-026d-489d-83cf-8b9c8740fcde")
                        },
                        new
                        {
                            ThemeColorId = new Guid("0f4edb89-3d2a-46f6-94d9-e11f0a3b9ca2"),
                            ColorId = new Guid("417bdb2b-e534-4925-b7c5-18a7bc89e752"),
                            ThemeId = new Guid("4a39c839-026d-489d-83cf-8b9c8740fcde")
                        });
                });

            modelBuilder.Entity("CleverAlbumDesigner.Models.Album", b =>
                {
                    b.HasOne("CleverAlbumDesigner.Models.Theme", "Theme")
                        .WithMany()
                        .HasForeignKey("ThemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Theme");
                });

            modelBuilder.Entity("CleverAlbumDesigner.Models.ThemeColor", b =>
                {
                    b.HasOne("CleverAlbumDesigner.Models.Color", "Color")
                        .WithMany()
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CleverAlbumDesigner.Models.Theme", "Theme")
                        .WithMany()
                        .HasForeignKey("ThemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Color");

                    b.Navigation("Theme");
                });
#pragma warning restore 612, 618
        }
    }
}
