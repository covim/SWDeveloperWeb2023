﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wifi.SD.Core.Entities.Movies
{
    public class MediumType
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

        public virtual ICollection<Movie> Movies { get; }
    }
}
