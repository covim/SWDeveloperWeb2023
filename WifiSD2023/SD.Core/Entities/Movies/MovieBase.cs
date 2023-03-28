using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wifi.SD.Core.Entities.Movies
{
    public abstract class MovieBase
    {
        [Key] // wäre hier nicht nötig das EF eine Guid mit name Id automatisch zum Primary Key macht
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(128)]
        public string Title { get; set; }

        [Column(TypeName ="date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateOnly ReleaseDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString ="{0:N}")]
        public decimal Price { get; set; }

        public int GenreId { get; set; }

        [MaxLength(8)]
        public string MediumTypeCode{ get; set;}


    }
}
