using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DuHoc.Data;
using DuHoc.Models;

namespace DuHoc.Controllers
{
    public class ParentCommentsController : Controller
    {
        private readonly DuHocContext _context;

        public ParentCommentsController(DuHocContext context)
        {
            _context = context;
        }

        // GET: ParentComments
        public async Task<IActionResult> Index()
        {
            var duHocContext = _context.ParentComment.Include(p => p.User);
            return View(await duHocContext.ToListAsync());
        }

        // GET: ParentComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ParentComment == null)
            {
                return NotFound();
            }

            var parentComment = await _context.ParentComment
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ParentComment_Id == id);
            if (parentComment == null)
            {
                return NotFound();
            }

            return View(parentComment);
        }

        // GET: ParentComments/Create
        public IActionResult Create()
        {
            ViewData["user_id"] = new SelectList(_context.User, "user_id", "user_id");
            return View();
        }

        // POST: ParentComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParentComment_Id,Text,Comment_Date,user_id")] ParentComment parentComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parentComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["user_id"] = new SelectList(_context.User, "user_id", "user_id", parentComment.user_id);
            return View(parentComment);
        }

        // GET: ParentComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ParentComment == null)
            {
                return NotFound();
            }

            var parentComment = await _context.ParentComment.FindAsync(id);
            if (parentComment == null)
            {
                return NotFound();
            }
            ViewData["user_id"] = new SelectList(_context.User, "user_id", "user_id", parentComment.user_id);
            return View(parentComment);
        }

        // POST: ParentComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ParentComment_Id,Text,Comment_Date,user_id")] ParentComment parentComment)
        {
            if (id != parentComment.ParentComment_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parentComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParentCommentExists(parentComment.ParentComment_Id))
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
            ViewData["user_id"] = new SelectList(_context.User, "user_id", "user_id", parentComment.user_id);
            return View(parentComment);
        }

        // GET: ParentComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ParentComment == null)
            {
                return NotFound();
            }

            var parentComment = await _context.ParentComment
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ParentComment_Id == id);
            if (parentComment == null)
            {
                return NotFound();
            }

            return View(parentComment);
        }

        // POST: ParentComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ParentComment == null)
            {
                return Problem("Entity set 'DuHocContext.ParentComment'  is null.");
            }
            var parentComment = await _context.ParentComment.FindAsync(id);
            if (parentComment != null)
            {
                _context.ParentComment.Remove(parentComment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParentCommentExists(int id)
        {
          return _context.ParentComment.Any(e => e.ParentComment_Id == id);
        }
    }
}
