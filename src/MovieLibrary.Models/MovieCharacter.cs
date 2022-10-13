using System.ComponentModel.DataAnnotations;

namespace MovieLibrary.Models
{
    public class MovieCharacter
    {
        [Required]
        public Guid MovieId { get; set; }
        public Movie? Movie { get; set; }

        [Required] 
        public Guid CharacterId { get; set; }
        public Character? Character { get; set; }
    }
}