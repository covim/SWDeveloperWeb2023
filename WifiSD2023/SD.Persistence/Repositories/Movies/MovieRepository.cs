using SD.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wifi.SD.Core.Repositories;
using Wifi.SD.Core.Attributes;

namespace SD.Persistence.Repositories.Movies
{
    [MapServiceDependency(nameof(MovieRepository))]
    public class MovieRepository : BaseRepository, IMovieRepository
    {
    }
}
