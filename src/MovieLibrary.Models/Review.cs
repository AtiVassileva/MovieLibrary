using System.ComponentModel.DataAnnotations;

namespace MovieLibrary.Models
{
    using static Common.DataConstants;
    public class Review
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string? Content { get; set; }

        [Required]
        public Guid MovieId { get; set; }
        public Movie? Movie { get; set; }

        [Required]
        public Guid AuthorId { get; set; }
        public User? Author { get; set; }
    }
}