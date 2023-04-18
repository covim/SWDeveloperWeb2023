using MediatR;
using SD.Application.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wifi.SD.Core.Application.Movies.Commands;
using Wifi.SD.Core.Application.Movies.Results;
using Wifi.SD.Core.Attributes;
using Wifi.SD.Core.Entities.Movies;
using Wifi.SD.Core.Repositories;

namespace SD.Application.Movies
{
    [MapServiceDependency(nameof(MovieCommandHandler))]
    public class MovieCommandHandler : HandlerBase, 
                                       IRequestHandler<CreateMovieDtoCommand, MovieDto>,
                                       IRequestHandler<UpdateMovieDtoCommand, MovieDto>,
                                       IRequestHandler<DeleteMovieDtoCommand, bool>
    {
        protected readonly IMovieRepository movieRepository;
        public MovieCommandHandler(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }
        public async Task<MovieDto> Handle(CreateMovieDtoCommand request, CancellationToken cancellationToken)
        {
            var movie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = "n/a",
                GenreId = 1,
                Rating = Ratings.Medium
            };

            await this.movieRepository.AddAsync(movie, true, cancellationToken);
            return MovieDto.MapFrom(movie);
        }

        public async Task<MovieDto> Handle(UpdateMovieDtoCommand request, CancellationToken cancellationToken)
        {
            request.MovieDto.Id = request.Id;

            var movie = new Movie();

            // jetzt kommt mapping per  refexion
            base.MapEntityProperties<MovieDto, Movie>(request.MovieDto, movie, null);
            var updatedMovie = await movieRepository.UpdateAsync<Movie>(movie, request.Id, true, cancellationToken);

            return MovieDto.MapFrom(updatedMovie);

        }

        public async Task<bool> Handle(DeleteMovieDtoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await this.movieRepository.RemoveAsyncByKey<Movie>(request.Id, true, cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
            
        }
    }
}
