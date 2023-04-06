using SD.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wifi.SD.Core.Attributes;
using Wifi.SD.Core.Repositories;

namespace SD.Persistence.Repositories.Movies
{
    [MapServiceDependency(nameof(GenreRepository))]
    internal class GenreRepository : BaseRepository, IGenreRepository
    {
    }
}
