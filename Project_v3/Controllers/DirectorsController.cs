using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Project_v3.Models;

namespace Project_v3.Controllers
{
    [Route("director")]
    [Authorize(Roles = "Moderator")]
    public class DirectorsController : Controller
    {
        private readonly AplicationDbContext _context;
        
        public DirectorsController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: Directors
        
        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {
              return View(await _context.Directors.ToListAsync());
        }
        
        [HttpGet("details")]
        // GET: Directors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Directors == null)
            {
                return NotFound();
            }

            var director = await _context.Directors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (director == null)
            {
                return NotFound();
            }

            return View(director);
        }
        
        [HttpGet("create")]
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Directors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDirector dir)
        {
            if (ModelState.IsValid)
            {
                Director dir1 = new Director()
                {
                    Name = dir.Name,
                    Surname = dir.surname,
                    fullname = $"{dir.Name} {dir.surname}"
                };
                _context.Add(dir1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Directors == null)
            {
                return NotFound();
            }

            var director = await _context.Directors.FindAsync(id);
            if (director == null)
            {
                return NotFound();
            }
            EditDirector editdirector = new EditDirector()
            {
                Name = director.Name,
                Surname = director.Surname
            };

            return View(editdirector);
        }

        // POST: Directors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute]int id, EditDirector director)
        {
            var check = _context.Directors.FirstOrDefault(x => x.Id == id);
            if (check == null)
            {
                return NotFound();
            }
            Director director2 = new Director()
            {
                Id = check.Id,
                Name = director.Name,
                Surname = director.Surname,
                fullname = check.fullname,
                Films = check.Films
            };
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Directors.Remove(check);
                    _context.Directors.Update(director2);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirectorExists(director2.Id))
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
            return View(director);
        }

        // GET: Directors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Directors == null)
            {
                return NotFound();
            }

            var director = await _context.Directors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (director == null)
            {
                return NotFound();
            }

            return View(director);
        }

        // POST: Directors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Directors == null)
            {
                return Problem("Entity set 'AplicationDbContext.Directors'  is null.");
            }
            var director = await _context.Directors.FindAsync(id);
            if (director != null)
            {
                _context.Directors.Remove(director);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DirectorExists(int id)
        {
          return _context.Directors.Any(e => e.Id == id);
        }
    }
}
