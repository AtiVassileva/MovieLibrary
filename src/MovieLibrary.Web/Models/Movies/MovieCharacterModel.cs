using System.ComponentModel;

namespace MovieLibrary.Web.Models.Movies
{
    public class MovieCharacterModel
    {
        public Guid Id { get; set; }
        [DisplayName("Image URL")]
        public string? ImageUrl { get; set; }
    }
}