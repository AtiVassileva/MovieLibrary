using System.ComponentModel.DataAnnotations;
using MovieLibrary.Web.Models.Genres;
using static MovieLibrary.Models.Common.DataConstants;

namespace MovieLibrary.Web.Models.Movies
{
    public class MovieFormModel
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = DefaultMinLength)]
        public string Title { get; set; } = null!;

        [Url]
        [Required]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [StringLength(NameMaxLength, MinimumLength = DefaultMinLength)]
        public string Director { get; set; } = null!;

        [Display(Name = "Premiere Date")]
        public DateTime PremiereDate { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DefaultMinLength)]
        public string Overview { get; set; } = null!;

        [Required]
        [Display(Name = "Genre")]
        public Guid GenreId { get; set; }

        public ICollection<GenreSelectionModel> Genres { get; set; }
            = new HashSet<GenreSelectionModel>();
    }
}