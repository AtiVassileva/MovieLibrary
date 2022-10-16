namespace MovieLibrary.Web.Models.Movies
{
    public class MovieListModel
    {
        public Guid Id { get; set; }
        public string? ImageUrl { get; set; }
        public string? Title { get; set; } 
        public int Likes { get; set; }
    }
}