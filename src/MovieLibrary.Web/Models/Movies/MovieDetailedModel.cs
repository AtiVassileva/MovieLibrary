namespace MovieLibrary.Web.Models.Movies
{
    public class MovieDetailedModel : MovieListModel
    {
        public string? Overview { get; set; }
        public string? Director { get; set; }
        public DateTime PremiereDate { get; set; }
        public string? GenreName { get; set; }
    }
}