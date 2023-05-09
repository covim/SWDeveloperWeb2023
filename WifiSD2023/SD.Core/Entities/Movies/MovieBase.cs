using Globalization;
using Globalization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wifi.SD.Core.Entities.Movies
{
    public enum Ratings : byte
    {
        //[LocalizedDescriptionAttribute(nameof(Ratings) + "_1")]
        [LocalizedDescription(nameof(BasicRes.Ratings_1))]
        VeryBad = 1,
        [LocalizedDescription(nameof(BasicRes.Ratings_2))]
        Bad,
        [LocalizedDescription(nameof(BasicRes.Ratings_3))]
        Medium,
        [LocalizedDescription(nameof(BasicRes.Ratings_4))]
        Good,
        [LocalizedDescription(nameof(BasicRes.Ratings_5))]
        VeryGood
    }


    public abstract class MovieBase
    {
        [Key] // wäre hier nicht nötig das EF eine Guid mit name Id automatisch zum Primary Key macht
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(128)]
        [Display(Name = nameof(MovieBase.Title), ResourceType = typeof(BasicRes))]
        public string Title { get; set; }

        [Column(TypeName ="date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = nameof(MovieBase.ReleaseDate), ResourceType = typeof(BasicRes))]
        public DateTime ReleaseDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString ="{0:N}")]
        [Display(Name = nameof(MovieBase.Price), ResourceType = typeof(BasicRes))]
        public decimal Price { get; set; }

        [Display(Name = "Genre", ResourceType = typeof(BasicRes))]
        public int GenreId { get; set; }

        [MaxLength(8)]
        [Display(Name = "MediumType", ResourceType = typeof(BasicRes))]
        public string? MediumTypeCode{ get; set;}

        [Display(Name = nameof(MovieBase.Rating), ResourceType = typeof(BasicRes))]
        public Ratings? Rating { get; set; }
    }
}
