using MovieLibrary.Web.Models.Reviews;

namespace MovieLibrary.Web.Models.Movies
{
    public class MovieDetailedModel : MovieListModel
    {
        public string? Overview { get; set; }
        public string? Director { get; set; }
        public DateTime PremiereDate { get; set; }
        public string? GenreName { get; set; }

        public IEnumerable<ReviewFormModel> Reviews { get; set; }
            = new HashSet<ReviewFormModel>();
    }
}