using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wifi.SD.Core.Application.Movies.Results;

namespace Wifi.SD.Core.Application.Movies.Queries
{
    public class GetMovieDtoQuery : IRequest<MovieDto>
    {
        public Guid Id { get; set; }
    }
}
