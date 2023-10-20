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
    public class ChildCommentsController : Controller
    {
        private readonly DuHocContext _context;

        public ChildCommentsController(DuHocContext context)
        {
            _context = context;
        }

        // GET: ChildComments
        public async Task<IActionResult> Index()
        {
            var duHocContext = _context.ChildComment.Include(c => c.User);
            return View(await duHocContext.ToListAsync());
        }

        // GET: ChildComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ChildComment == null)
            {
                return NotFound();
            }

            var childComment = await _context.ChildComment
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Comment_Id == id);
            if (childComment == null)
            {
                return NotFound();
            }

            return View(childComment);
        }

        // GET: ChildComments/Create
        public IActionResult Create()
        {
            ViewData["user_id"] = new SelectList(_context.User, "user_id", "user_id");
            return View();
        }

        // POST: ChildComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Comment_Id,Text,Comment_Date,user_id,ParentComment_Id")] ChildComment childComment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(childComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["user_id"] = new SelectList(_context.User, "user_id", "user_id", childComment.user_id);
            return View(childComment);
        }

        // GET: ChildComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ChildComment == null)
            {
                return NotFound();
            }

            var childComment = await _context.ChildComment.FindAsync(id);
            if (childComment == null)
            {
                return NotFound();
            }
            ViewData["user_id"] = new SelectList(_context.User, "user_id", "user_id", childComment.user_id);
            return View(childComment);
        }

        // POST: ChildComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Comment_Id,Text,Comment_Date,user_id,ParentComment_Id")] ChildComment childComment)
        {
            if (id != childComment.Comment_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(childComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChildCommentExists(childComment.Comment_Id))
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
            ViewData["user_id"] = new SelectList(_context.User, "user_id", "user_id", childComment.user_id);
            return View(childComment);
        }

        // GET: ChildComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ChildComment == null)
            {
                return NotFound();
            }

            var childComment = await _context.ChildComment
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Comment_Id == id);
            if (childComment == null)
            {
                return NotFound();
            }

            return View(childComment);
        }

        // POST: ChildComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ChildComment == null)
            {
                return Problem("Entity set 'DuHocContext.ChildComment'  is null.");
            }
            var childComment = await _context.ChildComment.FindAsync(id);
            if (childComment != null)
            {
                _context.ChildComment.Remove(childComment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChildCommentExists(int id)
        {
          return _context.ChildComment.Any(e => e.Comment_Id == id);
        }
    }
}
