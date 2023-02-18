using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_v3.Models;

namespace Project_v3.Controllers
{
    [Authorize]
    [Route("films")]
    public class FilmsController : Controller
    {
        private readonly AplicationDbContext _context;

        public FilmsController(AplicationDbContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        [HttpGet("Index")]
        public ActionResult<IEnumerable<Films>> Index()
        {
            var result = _context.Films.Include(x => x.Director).ToList();
            return View(result);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(int id)
        {
            var film = _context.Films.FirstOrDefault(o => o.Id == id);
            film.FilmCount++;
            _context.SaveChanges();
            
            return View();
            
        }
        
        // GET: Films/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Films == null)
            {
                return NotFound();
            }
            
            var films = await _context.Films
                .FirstOrDefaultAsync(m => m.Id == id);
            if (films == null)
            {
                return NotFound();
            }
            
            return View();
        }

        [Authorize(Roles = "Moderator")]
        [HttpGet("create")]
        public IActionResult Create()
        {
            var list = _context.Directors.ToList();
            List<string> fullname = new List<string>();
            foreach (var item in list)
            {
                fullname.Add(item.fullname);
            }
            CreateFilm director = new CreateFilm()
            {
                FullNames = fullname
            };
            return View(director);
        }

        // POST: Films/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Moderator")]
        [HttpPost("create")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(CreateFilm film)
        {
           
                var directors = _context.Directors.FirstOrDefault(x => x.fullname == film.selectedDirector);
                if (directors == null)
                {
                    return NotFound();
                }
                Films Cfilm = new Films()
                {
                    
                    FilmName = film.FilmName,
                    FilmDescription = film.FilmDescription,
                    FilmType = film.FilmType,
                    Directorfullname = directors.fullname,
                    
                };
                Cfilm.Director = directors;
                _context.Add(Cfilm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

        }
        [Authorize(Roles = "Moderator")]
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Films == null)
            {
                return NotFound();
            }

            var films = await _context.Films.FindAsync(id);
            if (films == null)
            {
                return NotFound();
            }
            return View(films);
        }


        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Moderator")]
        [HttpPost("edite/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FilmId,FilmName,FilmDescription,FilmType,FilmCount,DirectorId")] Films films)
        {
            if (id != films.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(films);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmsExists(films.Id))
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
            return View(films);
        }

        // GET: Films/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Films == null)
            {
                return NotFound();
            }

            var films = await _context.Films
                .FirstOrDefaultAsync(m => m.Id == id);
            if (films == null)
            {
                return NotFound();
            }
            
            return View();
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Films == null)
            {
                return Problem("Entity set 'AplicationDbContext.Films'  is null.");
            }
            var films = await _context.Films.FindAsync(id);
            if (films != null)
            {
                _context.Films.Remove(films);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmsExists(int id)
        {
            return _context.Films.Any(e => e.Id == id);
            
        }
    }
}
