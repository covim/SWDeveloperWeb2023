using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wifi.SD.Core.Attributes;
using Wifi.SD.Core.Entities.Movies;
using Wifi.SD.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Wifi.SD.Core.Application.Movies.Queries;
using Wifi.SD.Core.Application.Movies.Results;

namespace SD.Application.Movies
{
    [MapServiceDependency(nameof(MovieQueryHandler))]
    public class MovieQueryHandler : IRequestHandler<GetMovieDtoQuery, MovieDto>,
                                     IRequestHandler<GetMovieDtosQuery, IEnumerable<MovieDto>>,
                                     IRequestHandler<GetMediumTypesQuery, IEnumerable<MediumType>>,
                                     IRequestHandler<GetGenresQuery, IEnumerable<Genre>>
                                            
    {
        protected readonly IMovieRepository movieRepository;

        //public MovieQueryHandler(IServiceProvider serviceProvider)
        //{
        //    movieRepository = serviceProvider.GetRequiredService<IMovieRepository>();
        //}

        public MovieQueryHandler(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public async Task<MovieDto> Handle(GetMovieDtoQuery request, CancellationToken cancellationToken)
        {
            var movie = await this.GetMovieDtoQueryWithNavigationProperties()
                                                  .Where(w => w.Id == request.Id)
                                                  .FirstOrDefaultAsync(cancellationToken);

            if (movie != null)
            {
                return MovieDto.MapFrom(movie);
            }
            else
            {
                return null;
            }
        }

        private IQueryable<Movie> GetMovieDtoQueryWithNavigationProperties()
        {
            return this.movieRepository.QueryFrom<Movie>()
                                                 .Include(nameof(Genre))
                                                 .Include(nameof(MediumType));
        }

        public async Task<IEnumerable<MovieDto>> Handle(GetMovieDtosQuery request, CancellationToken cancellationToken)
        {

            var movieQuery = GetMovieDtoQueryWithNavigationProperties();

            if (request.GenreId != null)
            {
                movieQuery = movieQuery.Where(w => w.GenreId == request.GenreId);
            }

            if (!string.IsNullOrWhiteSpace(request.MediumTypeCode))
            {
                movieQuery = movieQuery.Where(w => w.MediumTypeCode == request.MediumTypeCode);
            }

            if (request.Ratings?.Count() > 0)
            {
                movieQuery = movieQuery.Where(w => request.Ratings.Contains(w.Rating.Value));
            }

            if (request.Take > 0)
            {
                movieQuery = movieQuery.Skip(request.Skip).Take(request.Take);  //Pager-Funktion in EF mit Linq
            }

            // so 
            var movieDtos = await movieQuery.Select(s => MovieDto.MapFrom(s)).ToListAsync(cancellationToken);

            /* oder
            //var movieDtos = new List<MovieDto>();
            //var movies = await movieQuery.ToListAsync(cancellationToken);

            //movies.ForEach(m => movieDtos.Add(MovieDto.MapFrom(m)));
            so */

            return movieDtos;
        }

        public async Task<IEnumerable<MediumType>> Handle(GetMediumTypesQuery request, CancellationToken cancellationToken)
        {
            return await this.movieRepository.QueryFrom<MediumType>().ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Genre>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
        {
            return await this.movieRepository.QueryFrom<Genre>().ToListAsync(cancellationToken);
        }
    }
}
