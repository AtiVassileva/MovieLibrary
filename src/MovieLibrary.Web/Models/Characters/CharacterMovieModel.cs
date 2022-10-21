using System.ComponentModel;

namespace MovieLibrary.Web.Models.Characters
{
    public class CharacterMovieModel
    {
        public Guid Id { get; set; }
        [DisplayName("Image URL")]
        public string? ImageUrl { get; set; }
        public string? Name { get; set; }
    }
}