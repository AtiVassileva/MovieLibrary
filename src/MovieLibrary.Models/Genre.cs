using System.ComponentModel.DataAnnotations;

namespace MovieLibrary.Models
{
    using static Common.DataConstants;
    public class Genre
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string? Name { get; set; }
    }
}