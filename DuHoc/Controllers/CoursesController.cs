﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DuHoc.Data;
using DuHoc.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace DuHoc.Controllers
{
    public class CoursesController : Controller
    {
        private readonly DuHocContext _context;

        public CoursesController(DuHocContext context)
        {
            _context = context;
        }

        // GET: Courses
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            var duHocContext = _context.Course.Include(c => c.University);
            return View(await duHocContext.ToListAsync());
        }

        // GET: Courses/Course
        public async Task<IActionResult> Course()
        {
            var duHocContext = _context.Course.Include(c => c.University);
            return View(await duHocContext.ToListAsync());
        }

        // GET: Courses/Details/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .Include(c => c.University)
                .FirstOrDefaultAsync(m => m.Course_Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewData["University_Id"] = new SelectList(_context.University, "University_Id", "Images");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("Course_Id,Title,Content,DayPosted,Scholarship,University_Id")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["University_Id"] = new SelectList(_context.University, "University_Id", "Images", course.University_Id);
            return View(course);
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["University_Id"] = new SelectList(_context.University, "University_Id", "Images", course.University_Id);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Course_Id,Title,Content,DayPosted,Scholarship,University_Id")] Course course)
        {
            if (id != course.Course_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Course_Id))
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
            ViewData["University_Id"] = new SelectList(_context.University, "University_Id", "Images", course.University_Id);
            return View(course);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Course == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .Include(c => c.University)
                .FirstOrDefaultAsync(m => m.Course_Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Course == null)
            {
                return Problem("Entity set 'DuHocContext.Course'  is null.");
            }
            var course = await _context.Course.FindAsync(id);
            if (course != null)
            {
                _context.Course.Remove(course);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
          return _context.Course.Any(e => e.Course_Id == id);
        }
    }
}
