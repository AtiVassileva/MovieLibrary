using System.ComponentModel.DataAnnotations;
using static MovieLibrary.Models.Common.DataConstants;

namespace MovieLibrary.Web.Models.Reviews
{
    public class ReviewFormModel
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = DefaultMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DefaultMinLength)]
        public string Content { get; set; } = null!;
    }
}