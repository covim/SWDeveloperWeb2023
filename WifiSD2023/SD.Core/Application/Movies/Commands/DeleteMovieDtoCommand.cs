
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wifi.SD.Core.Application.Movies.Commands
{
    public class DeleteMovieDtoCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
