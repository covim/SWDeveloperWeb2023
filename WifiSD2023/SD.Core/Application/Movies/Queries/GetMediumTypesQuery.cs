using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wifi.SD.Core.Application.Movies.Results;
using Wifi.SD.Core.Entities.Movies;

namespace Wifi.SD.Core.Application.Movies.Queries
{
    public class GetMediumTypesQuery : IRequest<IEnumerable<MediumType>>
    {
    }
}
