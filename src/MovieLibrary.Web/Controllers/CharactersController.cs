using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;
using MovieLibrary.Models;
using MovieLibrary.Web.Infrastructure;
using MovieLibrary.Web.Models.Characters;
using MovieLibrary.Web.Models.Movies;

namespace MovieLibrary.Web.Controllers
{
    public class CharactersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CharactersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<IActionResult> Index()
        {
            var characterModels = _mapper.Map<IEnumerable<CharacterListModel>>(_context.Characters.ToList());

            return Task.FromResult<IActionResult>(View(characterModels));
        }
        
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await _context.Characters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (character == null)
            {
                return NotFound();
            }

            var characterModel = _mapper.Map<CharacterDetailedModel>(character);
            characterModel.Movies = _context.MovieCharacters
                .Where(mch => mch.CharacterId == character.Id)
                .Select(x => new MovieCharacterModel
                {
                    Id = x.MovieId,
                    Title = x.Movie!.Title
                })
                .ToList();

            return View(characterModel);
        }
        
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name, ImageUrl,ActorName, Description")] CharacterFormModel characterModel)
        {
            if (!ModelState.IsValid)
            {
                return View(characterModel);
            }

            var character = _mapper.Map<Character>(characterModel);
            character.CreatorId = this.User.GetId();

            _context.Add(character);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await _context.Characters.FindAsync(id);

            if (character == null)
            {
                return NotFound();
            }

            var characterModel = _mapper.Map<CharacterFormModel>(character);
            return View(characterModel);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name. ImageUrl,Description,ActorName")] Character character)
        {
            if (id != character.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var characterModel = _mapper.Map<CharacterFormModel>(character);
                return View(characterModel);
            }
            try
            {
                _context.Update(character);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharacterExists(character.Id))
                {
                    return NotFound();
                }

                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await _context.Characters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (character == null)
            {
                return NotFound();
            }

            return View(character);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character != null)
            {
                _context.Characters.Remove(character);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharacterExists(Guid id)
        {
            return _context.Characters.Any(e => e.Id == id);
        }
    }
}