using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Wifi.SD.Core.Entities.Movies;

namespace Wifi.SD.Core.Application.Movies.Results
{
    /// <summary>
    /// Movie Data Transaction Object
    /// </summary>
    public class MovieDto : MovieBase
    {

        public static MovieDto MapFrom(Movie movie)
        {
            return new MovieDto
            {
                Id = movie.Id,
                Title = movie.Title,
                GenreId = movie.GenreId,
                MediumTypeCode = movie.MediumTypeCode,
                Price = movie.Price,
                Rating = movie.Rating,
                ReleaseDate = movie.ReleaseDate,
            };
        }

    }
}
