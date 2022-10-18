using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Web.Models;
using System.Diagnostics;
using AutoMapper;
using MovieLibrary.Data;
using MovieLibrary.Web.Models.Movies;
using AutoMapper.QueryableExtensions;

namespace MovieLibrary.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AutoMapper.IConfigurationProvider _configuration;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger, IMapper mapper)
        {
            _context = context;
            _configuration = mapper.ConfigurationProvider;
        }

        public IActionResult Index()
        {
            var latestMovies = _context.Movies.Take(3)
                .ProjectTo<MovieHomeModel>(_configuration)
                .ToList();
            
            return View(latestMovies);
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}