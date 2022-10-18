using System.ComponentModel.DataAnnotations;
using static MovieLibrary.Models.Common.DataConstants;

namespace MovieLibrary.Web.Models.Characters
{
    public class CharacterFormModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = DefaultMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [Url]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [StringLength(NameMaxLength, MinimumLength = DefaultMinLength)]
        public string ActorName { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DefaultMinLength)]
        public string Description { get; set; } = null!;
    }
}