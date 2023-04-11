using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Wifi.SD.Core.Attributes.Movies.Queries;
using Wifi.SD.Core.Attributes.Movies.Results;

namespace SD.WS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : MediatRBaseController
    {

        [HttpGet(nameof(MovieDto))]
        public async Task<IEnumerable<MovieDto>> GetMovieDtos([FromQuery] GetMovieDtosQuery query)
        {
            return await base.Mediator.Send(query);
        }


        [HttpGet(nameof(MovieDto) + "/{Id}")]
        public async Task<MovieDto> GetMovieDto([FromRoute] GetMovieDtoQuery query)
        {
            return await base.Mediator.Send(query);
        }

    }
}
