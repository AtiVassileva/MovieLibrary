using System.ComponentModel.DataAnnotations;

namespace MovieLibrary.Models
{
    public class User
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public string? Email { get; set; }

        public ICollection<Movie> LikedMovies { get; set; } = new HashSet<Movie>();

        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }
}