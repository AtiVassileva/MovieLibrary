using System.ComponentModel;
using MovieLibrary.Web.Models.Movies;

namespace MovieLibrary.Web.Models.Characters
{
    public class CharacterAssignModel : CharacterMovieModel
    {
        [DisplayName("Movie")]
        public Guid MovieId { get; set; }
        public IEnumerable<MovieCharacterModel> Movies { get; set; } 
            = new HashSet<MovieCharacterModel>();
    }
}