using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DuHoc.Data;
using DuHoc.Models;
using Microsoft.AspNetCore.Authorization;

namespace DuHoc.Controllers
{
    public class UniversitiesController : Controller
    {
        private readonly DuHocContext _context;

        public UniversitiesController(DuHocContext context)
        {
            _context = context;
        }

        // GET: Universities
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            var duHocContext = _context.University.Include(u => u.Country);
            return View(await duHocContext.ToListAsync());
        }

        // GET: Universities/University
        public async Task<IActionResult> University(string searchString)
        {
            var universities = from m in _context.University
                               select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                universities = universities.Where(s =>
                s.Name.Contains(searchString) ||
                s.Location.Contains(searchString));
            }
            return View(await universities.ToListAsync());
        }

        public async Task<IActionResult> University_Details(int? id)
        {
            if (id == null || _context.University == null)
            {
                return NotFound();
            }

            var university = await _context.University
                .Include(u => u.Country)
                .FirstOrDefaultAsync(m => m.University_Id == id);
            if (university == null)
            {
                return NotFound();
            }

            return View(university);
        }

        // GET: Universities/Details/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.University == null)
            {
                return NotFound();
            }

            var university = await _context.University
                .Include(u => u.Country)
                .FirstOrDefaultAsync(m => m.University_Id == id);
            if (university == null)
            {
                return NotFound();
            }

            return View(university);
        }

        // GET: Universities/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            var countries = _context.Country.ToList();
            ViewBag.Countries = new SelectList(countries, "Id", "Name"); 

            return View();
        }


        // POST: Universities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("University_Id,Name,Decription,TuitionFee,Location,Images,Id")] University university)
        {
            if (ModelState.IsValid)
            {
                _context.Add(university);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Country, "Id", "Id", university.Id);
            return View(university);
        }

        // GET: Universities/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            var countries = _context.Country.ToList();
            ViewBag.Countries = new SelectList(countries, "Id", "Name");
            if (id == null || _context.University == null)
            {
                return NotFound();
            }

            var university = await _context.University.FindAsync(id);
            if (university == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Country, "Id", "Id", university.Id);
            return View(university);
        }

        // POST: Universities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("University_Id,Name,Decription,TuitionFee,Location,Images,Id")] University university)
        {
            if (id != university.University_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(university);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UniversityExists(university.University_Id))
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
            ViewData["Id"] = new SelectList(_context.Country, "Id", "Id", university.Id);
            return View(university);

        }

        // GET: Universities/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.University == null)
            {
                return NotFound();
            }

            var university = await _context.University
                .Include(u => u.Country)
                .FirstOrDefaultAsync(m => m.University_Id == id);
            if (university == null)
            {
                return NotFound();
            }

            return View(university);
        }

        // POST: Universities/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.University == null)
            {
                return Problem("Entity set 'DuHocContext.University'  is null.");
            }
            var university = await _context.University.FindAsync(id);
            if (university != null)
            {
                _context.University.Remove(university);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UniversityExists(int id)
        {
          return _context.University.Any(e => e.University_Id == id);
        }
    }
}
