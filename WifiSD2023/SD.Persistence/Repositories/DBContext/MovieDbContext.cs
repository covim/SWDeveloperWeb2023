using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wifi.SD.Core.Entities;
using Wifi.SD.Core.Entities.Movies;

namespace SD.Persistence.Repositories.DBContext
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext() { }

        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
            Database.SetCommandTimeout(90);
        }


        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<MediumType> MediumTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable(nameof(Movie) + "s");
                //entity.HasKey(e => e.Id); Schlüssel definieren hier nicht notwendig da implizit
                entity.Property(x => x.Title).IsRequired().HasMaxLength(128);  //Überschreibt die Vorgabe aus dem MovieBase
                entity.HasIndex(x => x.Title).HasDatabaseName("IX_" + nameof(Movie) + "s_" + nameof(Movie.Title));
                entity.Property(x => x.ReleaseDate).HasColumnType("date");
                entity.Property(x => x.Price).HasPrecision(18, 2).HasDefaultValue(0M);

            });

            modelBuilder.Entity<MediumType>(entity =>
            {
                entity.ToTable(nameof(MediumType) + "s").HasKey(nameof(MediumType.Code));
            });

            //Foreign Key Constraint 0 : n
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.MediumType)
                .WithMany(m => m.Movies)
                .HasForeignKey(m => m.MediumType.Code)
                .OnDelete(DeleteBehavior.SetNull);  //löschweitergabe => Wert in Movie auf Null setzen

            //Foreign Key Constraint 1 : n
            modelBuilder.Entity<Genre>().ToTable(nameof(Genre) + "s")
                .HasMany(g => g.Movies)
                .WithOne(g => g.Genre)
                .HasForeignKey(g => g.GenreId)
                .OnDelete(DeleteBehavior.Restrict); //keine Löschweitergabe


            //seed Methods - Set Dafault Database entries

            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action" },
                new Genre { Id = 2, Name = "Horror" },
                new Genre { Id = 3, Name = "Science Fiction" },
                new Genre { Id = 4, Name = "Comedy" }
                );

            modelBuilder.Entity<MediumType>().HasData(
                new MediumType { Code = "VHS", Name = "Videokasette" },
                new MediumType { Code = "DVD", Name = "Digital Versatile Disk" },
                new MediumType { Code = "BR", Name = "Blu-Ray" },
                new MediumType { Code = "BR3", Name = "3d Blu-Ray" },
                new MediumType { Code = "BRHD", Name = "HD Blu-Ray" },
                new MediumType { Code = "BR4K", Name = "4K Blu-Ray" }
                );

            modelBuilder.Entity<Movie>().HasData(
                new Movie
                {
                    Id = new Guid("165bbd6d - d2de - 434b - a3fb - 908b8606df64"),
                    Title = "Rambo",
                    Price = 4.9M,
                    MediumTypeCode = "VHS",
                    ReleaseDate = new DateOnly(1985, 4, 3),
                    GenreId = 1

                },

                new Movie
                {
                    Id = new Guid("e1229a0e-b210-4151-957a-34f9e5ba299a"),
                    Title = "Star Trek - Beyond",
                    Price = 12.9M,
                    MediumTypeCode = "BR3D",
                    ReleaseDate = new DateOnly(2016, 7, 1),
                    GenreId = 3

                },

                new Movie
                {
                    Id = new Guid("1ea14dbc-996f-4bea-815d-c92850ec8645"),
                    Title = "Star Wars - Episode IV",
                    Price = 9.9M,
                    MediumTypeCode = "DVD",
                    ReleaseDate = new DateOnly(1987, 4, 13),
                    GenreId = 3

                },

                new Movie
                {
                    Id = new Guid("b357da7d-5141-4e71-9d73-9c642c081be0"),
                    Title = "The Ring",
                    Price = 9.7M,
                    MediumTypeCode = "BR",
                    ReleaseDate = new DateOnly(2005, 11, 15),
                    GenreId = 2

                }

                );
        }

    }
}
