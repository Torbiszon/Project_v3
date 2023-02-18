using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private UserManager<AppUser> _userManager;
        private readonly AplicationDbContext _context;

        public FilmsController(AplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [AllowAnonymous]
        [HttpGet("Index")]
        public ActionResult<IEnumerable<Films>> Index()
        {
            var result = _context.Films.Include(x => x.Director).Include(f => f.Users).ToList();

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
        [AllowAnonymous]
        [HttpGet("details/{id}")]
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

            return View(films);
        }
        [Authorize(Roles = "User, Moderator")]
        public async Task<ActionResult> Rank(int id)
        {
            var film = _context.Films.Include(f => f.Users).FirstOrDefault(x => x.Id == id);
            var user = await _userManager.GetUserAsync(User);
            var user1 = _context.Users.Include(x => x.films).FirstOrDefault(f => f.Id == user.Id);
            if (film != null && user != null)
            {
                if (film.Users.IndexOf(user1) >= 0)
                {
                    film.Users.Remove(user1);
                    user1.films.Remove(film);
                    if (film.FilmCount > 0)
                    {
                        film.FilmCount--;
                    }
                }
                else
                {
                    user1.films.Add(film);
                    film.Users.Add(user1);
                    film.FilmCount++;
                }
            }
            else
            {
                return NotFound();
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet("ranking")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<Films>> Ranking()
        {
            var result = _context.Films.Include(x => x.Director).OrderByDescending(s => s.FilmCount).ToList();
            return View(result);
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
            EditFilm editfilm = new EditFilm()
            {
                FilmName = films.FilmName,
                FilmDescription = films.FilmDescription,
                FilmType = films.FilmType
            };
            return View(editfilm);
        }


        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Moderator")]
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, EditFilm film)
        {
            var check = _context.Films.FirstOrDefault(x => x.Id == id);
            if (check == null)
            {
                return NotFound();
            }
            Films films = new Films()
            {
                Id = check.Id,
                FilmName = film.FilmName,
                FilmDescription = film.FilmDescription,
                FilmType = film.FilmType,
                FilmCount = check.FilmCount,
                Directorfullname = check.Directorfullname,
                Director = check.Director,
                Users = check.Users
            };
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Films.Remove(check);
                    _context.Films.Update(films);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmsExists(id))
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
            return View(film);
        }
        [Authorize(Roles = "Moderator")]
        [HttpGet("delete/{id}")]
        
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

            return View(films);
        }

        // POST: Films/Delete/5
        [Authorize(Roles = "Moderator")]
        [HttpPost("delete/{id}")]
        
        public async Task<IActionResult> Delete([FromRoute]int id)
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
