using System.ComponentModel;
using MovieLibrary.Web.Models.Reviews;

namespace MovieLibrary.Web.Models.Movies
{
    public class MovieDetailedModel : MovieListModel
    {
        public string? Overview { get; set; }
        public string? Director { get; set; }
        [DisplayName("Premiere Date")]
        public DateTime PremiereDate { get; set; }
        [DisplayName("Genre")]
        public string? GenreName { get; set; }
        public string? CreatorId { get; set; }
        public IEnumerable<ReviewFormModel> Reviews { get; set; }
            = new HashSet<ReviewFormModel>();
    }
}