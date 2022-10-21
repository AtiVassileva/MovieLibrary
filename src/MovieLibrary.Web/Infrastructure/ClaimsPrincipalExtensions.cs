using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using MovieLibrary.Models;

namespace MovieLibrary.Web.Infrastructure
{
    public static class ClaimsPrincipalExtensions
    {
        public static string? GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public static bool HasLikedAMovie(this ClaimsPrincipal user, Guid? movieId, 
            UserManager<User> manager)
            => manager.GetUserAsync(user).Result.LikedMovies.Any(m => m.Id == movieId);
    }
}