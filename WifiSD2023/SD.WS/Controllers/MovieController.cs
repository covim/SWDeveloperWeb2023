using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Net;
using Wifi.SD.Core.Application.Movies.Commands;
using Wifi.SD.Core.Application.Movies.Queries;
using Wifi.SD.Core.Application.Movies.Results;

namespace SD.WS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : MediatRBaseController
    {

        [HttpGet(nameof(MovieDto))]
        public async Task<IEnumerable<MovieDto>> GetMovieDtos([FromQuery] GetMovieDtosQuery query, CancellationToken cancellationToken)
        {
            return await base.Mediator.Send(query);
        }


        [HttpGet(nameof(MovieDto) + "/{Id}")]
        public async Task<MovieDto> GetMovieDto([FromRoute] GetMovieDtoQuery query, CancellationToken cancellationToken)
        {
            return await base.Mediator.Send(query);
        }

        [ProducesResponseType(typeof(MovieDto), (int)HttpStatusCode.Created)]
        [HttpPost(nameof(MovieDto))]
        public async Task<MovieDto> CreateMovieDto(CancellationToken cancellationToken)
        {
            var createMovieDtoCommand = new CreateMovieDtoCommand();
            var result = await base.Mediator.Send(createMovieDtoCommand, cancellationToken);

            base.SetLocationUri(result, result.Id.ToString());
            return result;
            
        }

        [HttpPut(nameof(MovieDto) + "/{Id}")]
        public async Task<MovieDto> UpdateMovieDto([FromRoute] Guid Id, [FromBody] MovieDto movieDto, CancellationToken cancellationToken)
        {
            var updateMovieDtoCommand = new UpdateMovieDtoCommand { Id = Id, MovieDto = movieDto };
            
            var result = await base.Mediator.Send(updateMovieDtoCommand, cancellationToken);
            return result;

        }

        [HttpDelete(nameof(MovieDto) + "/{Id}")]
        public async Task<bool> DeleteMovieDto([FromRoute] Guid Id, CancellationToken cancellationToken)
        {
            var deleteMovieDtoCommand = new DeleteMovieDtoCommand { Id = Id};

            var result = await base.Mediator.Send(deleteMovieDtoCommand, cancellationToken);
            return result;

        }

    }
}
