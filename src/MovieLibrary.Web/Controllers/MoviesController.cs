using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;
using MovieLibrary.Models;
using MovieLibrary.Web.Infrastructure;
using MovieLibrary.Web.Models.Characters;
using MovieLibrary.Web.Models.Genres;
using MovieLibrary.Web.Models.Movies;
using MovieLibrary.Web.Models.Reviews;

namespace MovieLibrary.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly AutoMapper.IConfigurationProvider _configuration;

        public MoviesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _configuration = mapper.ConfigurationProvider;
        }
        
        public Task<IActionResult> Index()
        {
            var movieListModels =  _mapper
                .Map<IEnumerable<MovieListModel>>
                    (_context.Movies.ToList());
            
            return Task.FromResult<IActionResult>(View(movieListModels));
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

            var movieModel = await _context.Movies
                .ProjectTo<MovieDetailedModel>(_configuration)
                .FirstOrDefaultAsync(m => m.Id == movie.Id);

            movieModel!.Reviews = await _context.Reviews
                .Where(r => r.MovieId == movie.Id)
                .ProjectTo<ReviewFormModel>(_configuration)
                .ToListAsync();

            movieModel!.Characters = await _context.MovieCharacters
                    .Where(ch => ch.MovieId == movie.Id)
                .ProjectTo<CharacterMovieModel>(_configuration)
                .ToListAsync();

            return View(movieModel);
        }

        public async Task<IActionResult> Like(Guid id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            var currentUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == this.User.GetId());

            if (currentUser == null)
            {
                return Unauthorized();
            }

            movie.Likes++;
            currentUser.LikedMovies.Add(movie);

            await _context.SaveChangesAsync();

            var movieModel = _mapper.Map<MovieDetailedModel>(movie);
            return View(nameof(Details), movieModel);
        }

        public IActionResult Create()
        {
            return View(new MovieFormModel
            {
                Genres = _mapper
                    .Map<IEnumerable<GenreSelectionModel>>
                        (_context.Genres.ToList())
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

            var movie = _mapper.Map<Movie>(movieFormModel);
            movie.CreatorId = this.User.GetId();
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

            var movieModel = _mapper.Map<MovieFormModel>(movie);
            movieModel.Genres = _mapper
                .Map<IEnumerable<GenreSelectionModel>>
                    (_context.Genres.ToList());

            return View(movieModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Overview,ImageUrl,PremiereDate,Director,Likes, GenreId, CreatorId")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var movieModel = _mapper.Map<MovieFormModel>(movie);
                movieModel.Genres = _mapper
                    .Map<IEnumerable<GenreSelectionModel>>
                        (_context.Genres.ToList());
                return View(movieModel);
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
            return RedirectToAction(nameof(Details), new {id = movie.Id});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(Guid movieId,
            [Bind("Title, Content")] ReviewFormModel reviewModel)
        {
            var movie = await _context.Movies.FindAsync(movieId);

            if (movie == null)
            {
                return NotFound();
            }

            var movieModel = _mapper.Map<MovieDetailedModel>(movie);

            movieModel.Reviews = _mapper
                .Map<IEnumerable<ReviewFormModel>>(_context.Reviews
                    .Where(r => r.MovieId == movie.Id)
                    .ToList());

            if (!ModelState.IsValid)
            {
                return View(nameof(Details), movieModel);
            }

            var review = _mapper.Map<Review>(reviewModel);
            review.MovieId = movieId;
            review.AuthorId = this.User.GetId();

            movie.Reviews.Add(review);
            _context.Add(review);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = movieModel.Id });
        }

        public async Task<IActionResult> DeleteReview(Guid id)
        {
            var review = await _context.Reviews
                .FirstOrDefaultAsync(r => r.Id == id);

            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = review.MovieId });
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            var movieModel = _mapper.Map<MovieDetailedModel>(movie);
            return View(movieModel);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
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