using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Web.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;
using MovieLibrary.Web.Models.Movies;

namespace MovieLibrary.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var latestMovies = _context.Movies
                .Take(3)
                .Select(m => new MovieHomeModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    Overview = m.Overview,
                    ImageUrl = m.ImageUrl,
                    GenreName =  _context.Genres
                        .Where(g => g.Id == m.GenreId)
                        .Select(g => g.Name)
                        .FirstOrDefault()
                });

            return View(latestMovies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}