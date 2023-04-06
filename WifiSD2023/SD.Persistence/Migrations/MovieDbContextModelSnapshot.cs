﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SD.Persistence.Repositories.DBContext;

#nullable disable

namespace SD.Persistence.Migrations
{
    [DbContext(typeof(MovieDbContext))]
    partial class MovieDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Wifi.SD.Core.Entities.Movies.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Genres", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Horror"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Science Fiction"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Comedy"
                        });
                });

            modelBuilder.Entity("Wifi.SD.Core.Entities.Movies.MediumType", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Code");

                    b.ToTable("MediumTypes", (string)null);

                    b.HasData(
                        new
                        {
                            Code = "VHS",
                            Name = "Videokasette"
                        },
                        new
                        {
                            Code = "DVD",
                            Name = "Digital Versatile Disk"
                        },
                        new
                        {
                            Code = "BR",
                            Name = "Blu-Ray"
                        },
                        new
                        {
                            Code = "BR3D",
                            Name = "3d Blu-Ray"
                        },
                        new
                        {
                            Code = "BRHD",
                            Name = "HD Blu-Ray"
                        },
                        new
                        {
                            Code = "BR4K",
                            Name = "4K Blu-Ray"
                        });
                });

            modelBuilder.Entity("Wifi.SD.Core.Entities.Movies.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<string>("MediumTypeCode")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<decimal>("Price")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m);

                    b.Property<byte?>("Rating")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("MediumTypeCode");

                    b.HasIndex("Title")
                        .HasDatabaseName("IX_Movies_Title");

                    b.ToTable("Movies", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("165bbd6d-d2de-434b-a3fb-908b8606df64"),
                            GenreId = 1,
                            MediumTypeCode = "VHS",
                            Price = 4.9m,
                            ReleaseDate = new DateTime(1985, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Rambo"
                        },
                        new
                        {
                            Id = new Guid("e1229a0e-b210-4151-957a-34f9e5ba299a"),
                            GenreId = 3,
                            MediumTypeCode = "BR3D",
                            Price = 12.9m,
                            ReleaseDate = new DateTime(2016, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Star Trek - Beyond"
                        },
                        new
                        {
                            Id = new Guid("1ea14dbc-996f-4bea-815d-c92850ec8645"),
                            GenreId = 3,
                            MediumTypeCode = "DVD",
                            Price = 9.9m,
                            ReleaseDate = new DateTime(1987, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Star Wars - Episode IV"
                        },
                        new
                        {
                            Id = new Guid("b357da7d-5141-4e71-9d73-9c642c081be0"),
                            GenreId = 2,
                            MediumTypeCode = "BR",
                            Price = 9.7m,
                            ReleaseDate = new DateTime(2005, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "The Ring"
                        });
                });

            modelBuilder.Entity("Wifi.SD.Core.Entities.Movies.Movie", b =>
                {
                    b.HasOne("Wifi.SD.Core.Entities.Movies.Genre", "Genre")
                        .WithMany("Movies")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Wifi.SD.Core.Entities.Movies.MediumType", "MediumType")
                        .WithMany("Movies")
                        .HasForeignKey("MediumTypeCode")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Genre");

                    b.Navigation("MediumType");
                });

            modelBuilder.Entity("Wifi.SD.Core.Entities.Movies.Genre", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("Wifi.SD.Core.Entities.Movies.MediumType", b =>
                {
                    b.Navigation("Movies");
                });
#pragma warning restore 612, 618
        }
    }
}
