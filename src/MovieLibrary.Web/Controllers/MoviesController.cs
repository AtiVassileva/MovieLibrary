using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;
using MovieLibrary.Models;
using MovieLibrary.Web.Models.Movies;

namespace MovieLibrary.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies.ToListAsync();

            var movieListModels = movies
                .Select(movie => new MovieListModel
                {
                    Id = movie.Id,
                    ImageUrl = movie.ImageUrl, 
                    Title = movie.Title, 
                    Likes = movie.Likes
                }).ToList();

            return View(movieListModels);
        }
        
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return View("Error");
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return View("Error");
            }

            var movieModel = new MovieDetailedModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Overview = movie.Overview,
                ImageUrl = movie.ImageUrl,
                Director = movie.Director,
                PremiereDate = movie.PremiereDate,
                Likes = movie.Likes
            };

            return View(movieModel);
        }
        
        public IActionResult Create() => View();
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Overview,ImageUrl,PremiereDate,Director,Likes")] Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return View(movie);
            }

            movie.Id = Guid.NewGuid();
            _context.Add(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return View("Error");
            }

            var movie = await _context.Movies.FindAsync(id);
            return movie == null ? View("Error") : View(movie);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Overview,ImageUrl,PremiereDate,Director,Likes")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(Guid movieId,
            [Bind("Id,Title, Content")] Review review)
        {
            var movie = await _context.Movies.FindAsync(movieId);

            if (!ModelState.IsValid)
            {
                return View(nameof(Details), movie);
            }

            review.Id = Guid.NewGuid();
            review.MovieId = movieId;
            review.AuthorId = Guid.Parse("921D27B9-8A03-4E8D-9CC5-50EF8F744F2F");

            _context.Add(review);
            await _context.SaveChangesAsync();

            return View(nameof(Details), movie);
        }
        
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Movies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Movies'  is null.");
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(Guid id)
        {
          return _context.Movies.Any(e => e.Id == id);
        }
    }
}