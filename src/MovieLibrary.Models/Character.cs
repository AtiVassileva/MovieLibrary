using System.ComponentModel.DataAnnotations;

namespace MovieLibrary.Models
{
    using static Common.DataConstants;
    public class Character
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string? Name { get; set; }

        [MaxLength(DescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        public string? ImageUrl { get; set; }

        public string? ActorName { get; set; }

        public string? CreatorId { get; set; }

        public User? Creator { get; set; }

        public ICollection<MovieCharacter> MoviesCharacters { get; set; }
            = new HashSet<MovieCharacter>();
    }
}