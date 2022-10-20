using Microsoft.AspNetCore.Identity;

namespace MovieLibrary.Models
{
    public class User : IdentityUser
    {
        public ICollection<Movie> AddedMovies { get; set; } = new HashSet<Movie>();

        public ICollection<Movie> LikedMovies { get; set; } = new HashSet<Movie>();

        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }
}