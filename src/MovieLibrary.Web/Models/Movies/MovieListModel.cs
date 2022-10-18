using System.ComponentModel;

namespace MovieLibrary.Web.Models.Movies
{
    public class MovieListModel
    {
        public Guid Id { get; set; }
        [DisplayName("Image URL")]
        public string? ImageUrl { get; set; }
        public string? Title { get; set; } 
        public int Likes { get; set; }
    }
}