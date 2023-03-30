using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
}
