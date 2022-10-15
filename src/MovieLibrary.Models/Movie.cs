using System.ComponentModel.DataAnnotations;

namespace MovieLibrary.Models
{
    using static Common.DataConstants;
    public class Movie
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string? Overview { get; set; }

        [Required]
        public string? ImageUrl { get; set; }

        public DateTime PremiereDate { get; set; }

        public string? Director { get; set; }

        public int Likes { get; set; }

        public Guid? CreatorId { get; set; }

        public User? Creator { get; set; }

        public ICollection<Genre> Genres { get; set; } = new HashSet<Genre>();

        public ICollection<MovieCharacter> MoviesCharacters { get; set; }
            = new HashSet<MovieCharacter>();
    }
}