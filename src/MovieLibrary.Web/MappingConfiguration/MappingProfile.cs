using AutoMapper;
using MovieLibrary.Models;
using MovieLibrary.Web.Models.Characters;
using MovieLibrary.Web.Models.Genres;
using MovieLibrary.Web.Models.Movies;
using MovieLibrary.Web.Models.Reviews;

namespace MovieLibrary.Web.MappingConfiguration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // genres
            this.CreateMap<Genre, GenreSelectionModel>()
                .ReverseMap();

            // movies
            this.CreateMap<Movie, MovieHomeModel>()
                .ForMember(m => m.GenreName, cfg => cfg.MapFrom(m => m.Genre!.Name))
                .ReverseMap();

            this.CreateMap<Movie, MovieListModel>()
                .ReverseMap();

            this.CreateMap<Movie, MovieFormModel>()
                .ReverseMap();

            this.CreateMap<Movie, MovieDetailedModel>()
                .ForMember(model => model.GenreName, cfg => cfg.MapFrom(m => m.Genre!.Name))
                .ReverseMap();

            // reviews
            this.CreateMap<Review, ReviewFormModel>()
                .ReverseMap();

            // characters
            this.CreateMap<Character, CharacterListModel>()
                .ReverseMap();

            this.CreateMap<Character, CharacterFormModel>()
                .ReverseMap();

            this.CreateMap<Character, CharacterDetailedModel>()
                .ReverseMap();
        }
    }
}