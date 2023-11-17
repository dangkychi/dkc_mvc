using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DuHoc.Data;
using DuHoc.Models;
using DuHoc.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using NuGet.Protocol.Plugins;

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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            var duHocContext = _context.ParentComment.Include(p => p.User);
            return View(await duHocContext.ToListAsync());
        }

        // GET: ParentComments/Details/5
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("ParentComment_Id,Text,user_name,Comment_Date,user_id")] ParentComment parentComment)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Post(string Text, NewsListViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var currentName = User.Identity.Name;
                    var user = _context.User.SingleOrDefault(u => u.user_name == currentName);


                    if (user != null)
                    {
                        var parentComment = new ParentComment
                        {
                            user_id = user.user_id,
                            user_name = currentName,
                            Comment_Date = DateTime.Now,
                            Text = Text
                        };

                        _context.ParentComment.Add(parentComment);
                        await _context.SaveChangesAsync();

                        return RedirectToAction("News", "NewsPosts");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Bạn cần đăng nhập để có thể bình luận.");
                    }

                }
            }
            if (!User.Identity.IsAuthenticated)
            {
                TempData["CommentError"] = "Bạn cần đăng nhập để bình luận";
                return RedirectToAction("News", "NewsPosts");
            }
            return View();
        }

        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Post(string Text)
        {
            if (ModelState.IsValid)
            {
                var currentName = User.Identity.Name;
                var user = _context.User.SingleOrDefault(u => u.user_name == currentName);

                if (user != null)
                {
                    var parentComment = new ParentComment
                    {
                        user_id = user.user_id,
                        Comment_Date = DateTime.Now,
                        Text = Text
                    };

                    _context.ParentComment.Add(parentComment);
                    _context.SaveChangesAsync();

                    return Ok();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Bạn cần đăng nhập để có thể bình luận.");
                }
            }
            return Ok();
        }*/

        public async Task<IActionResult> Delete_Comment(int? id)
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
        [HttpPost, ActionName("Delete_Comment")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed_Comment(int id)
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
            return RedirectToAction("News", "NewsPosts");
        }


        // GET: ParentComments/Edit/5
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ParentComment_Id,Text,user_name,Comment_Date,user_id")] ParentComment parentComment)
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
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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
