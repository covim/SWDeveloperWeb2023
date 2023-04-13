using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wifi.SD.Core.Application.Movies.Results;

namespace Wifi.SD.Core.Application.Movies.Commands
{
    public class CreateMovieDtoCommand : IRequest<MovieDto> { }

}
