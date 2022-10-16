namespace MovieLibrary.Web.Models.Movies
{
    public class MovieHomeModel
    {
        public Guid Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? Title { get; set; }
        public string? Overview { get; set; }
        public string? GenreName { get; set; }
    }
}