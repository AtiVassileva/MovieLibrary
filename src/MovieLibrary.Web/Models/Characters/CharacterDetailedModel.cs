namespace MovieLibrary.Web.Models.Characters
{
    public class CharacterDetailedModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? ActorName { get; set; }
        public string? Description { get; set; }
        public string? CreatorId { get; set; }
    }
}