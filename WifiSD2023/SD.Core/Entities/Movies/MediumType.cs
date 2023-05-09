using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Wifi.SD.Core.Entities.Movies
{
    public class MediumType : IEntity
    {
        public MediumType()
        {
            this.Movies = new HashSet<Movie>();
        }

        [Key]
        [MaxLength(8)]
        public virtual string Code { get; set; }

        [MaxLength(32), MinLength(2)]
        public virtual string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Movie> Movies { get; }
    }
}
