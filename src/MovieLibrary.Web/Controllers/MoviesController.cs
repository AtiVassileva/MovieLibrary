using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;
using MovieLibrary.Models;
using MovieLibrary.Web.Models.Genres;
using MovieLibrary.Web.Models.Movies;
using MovieLibrary.Web.Models.Reviews;

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
                Likes = movie.Likes,
                GenreName =  await _context.Genres
                    .Where(g => g.Id == movie.GenreId)
                    .Select(g => g.Name)
                    .FirstOrDefaultAsync(),
                Reviews =  _context.Reviews
                    .Where(r => r.MovieId == movie.Id)
                    .Select(r => new ReviewFormModel
                    {
                        Title = r.Title!,
                        Content = r.Content!
                    })
                    .ToList()
            };

            return View(movieModel);
        }
        
        public IActionResult Create()
        {
            return View(new MovieFormModel
            {
                Genres = _context.Genres
                    .Select(g => new GenreSelectionModel
                    {
                        Id = g.Id,
                        Name = g.Name
                    }).ToList()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Overview,ImageUrl,PremiereDate,Director, GenreId")] MovieFormModel movieFormModel)
        {
            if (!ModelState.IsValid)
            {
                return View(movieFormModel);
            }

            var movie = new Movie
            {
                Id = Guid.NewGuid(),
                Title = movieFormModel.Title,
                Overview = movieFormModel.Overview,
                Director = movieFormModel.Director,
                ImageUrl = movieFormModel.ImageUrl,
                PremiereDate = movieFormModel.PremiereDate,
                GenreId = movieFormModel.GenreId
            };

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

            if (movie == null)
            {
                return View("Error");

            }

            return View(new MovieFormModel
            {
                Id = movie.Id,
                Title = movie.Title!,
                Overview = movie.Overview!,
                Director = movie.Director!,
                ImageUrl = movie.ImageUrl!,
                PremiereDate = movie.PremiereDate,
                GenreId = movie.GenreId,
                Genres = _context.Genres
                    .Select(g => new GenreSelectionModel
                    {
                        Id = g.Id,
                        Name = g.Name
                    }).ToList()
            });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Overview,ImageUrl,PremiereDate,Director,Likes, GenreId")] MovieFormModel movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(movie);
            }
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

                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(Guid movieId,
            [Bind("Title, Content")] ReviewFormModel reviewModel)
        {
            var movie = await _context.Movies.FindAsync(movieId);

            var movieModel = new MovieDetailedModel
            {
                Id = movie!.Id,
                Title = movie.Title,
                Overview = movie.Overview,
                ImageUrl = movie.ImageUrl,
                Director = movie.Director,
                PremiereDate = movie.PremiereDate,
                Likes = movie.Likes,
                GenreName = await _context.Genres.Where(g => g.Id == movie.GenreId)
                    .Select(g => g.Name)
                    .FirstOrDefaultAsync(),
                Reviews = _context.Reviews
                    .Where(r => r.MovieId == movie.Id)
                    .Select(r => new ReviewFormModel
                    {
                        Title = r.Title!,
                        Content = r.Content!
                    })
                    .ToList()
            };

            if (!ModelState.IsValid)
            {
                return View(nameof(Details), movieModel);
            }

            var review = new Review
            {
                Id = Guid.NewGuid(),
                Title = reviewModel.Title,
                Content = reviewModel.Content,
                MovieId = movieId,
                AuthorId = Guid.Parse("921D27B9-8A03-4E8D-9CC5-50EF8F744F2F")
            };

            movie.Reviews.Add(review);
            _context.Add(review);
            await _context.SaveChangesAsync();

            return View(nameof(Details), movieModel);
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