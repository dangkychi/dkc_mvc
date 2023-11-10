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
using System.Data;
using System.Xml.Linq;
using DuHoc.Models.ViewModels;

namespace DuHoc.Controllers
{
    public class NewsPostsController : Controller
    {
        private readonly DuHocContext _context;
        public int PageSize = 6;

        public NewsPostsController(DuHocContext context)
        {
            _context = context;
        }

        // GET: NewsPosts
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
              return View(await _context.NewsPost.ToListAsync());
        }

        // GET: NewsPosts/News
        public IActionResult News(int newsPage = 1)
        {
            return View(new NewsListViewModel
            {
                Users = _context.User,
                ParentComments = _context.ParentComment.OrderByDescending(y => y.ParentComment_Id),
                NewsPosts = _context.NewsPost.OrderByDescending(y => y.News_Id).Skip((newsPage - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    ItemsPerPage= PageSize,
                    CurrentPage = newsPage,
                    TotalItems = _context.NewsPost.Count()
                }
            });
        }

        // GET: NewsPosts/Details/5
        public async Task<IActionResult> News_Details(int? id)
        {
            if (id == null || _context.NewsPost == null)
            {
                return NotFound();
            }

            var newsPost = await _context.NewsPost
                .FirstOrDefaultAsync(m => m.News_Id == id);
            if (newsPost == null)
            {
                return NotFound();
            }

            return View(newsPost);
        }

        /*public ActionResult News_Comment()
        {
            var comments = "hh";

            var model = new NewsPost
            {
                Comments = comments
            };

            return View(model);
        }*/

        // GET: NewsPosts/Details/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NewsPost == null)
            {
                return NotFound();
            }

            var newsPost = await _context.NewsPost
                .FirstOrDefaultAsync(m => m.News_Id == id);
            if (newsPost == null)
            {
                return NotFound();
            }

            return View(newsPost);
        }

        // GET: NewsPosts/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: NewsPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("News_Id,Title,UserPosted,Content,Images")] NewsPost newsPost)
        {
            if (ModelState.IsValid)
            {
                newsPost.TimePosted = DateTime.Now;
                _context.Add(newsPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newsPost);
        }


        // GET: NewsPosts/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.NewsPost == null)
            {
                return NotFound();
            }

            var newsPost = await _context.NewsPost.FindAsync(id);
            if (newsPost == null)
            {
                return NotFound();
            }
            return View(newsPost);
        }

        // POST: NewsPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("News_Id,Title,TimePosted,UserPosted,Content,Images")] NewsPost newsPost)
        {
            if (id != newsPost.News_Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newsPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsPostExists(newsPost.News_Id))
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
            return View(newsPost);
        }

        // GET: NewsPosts/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.NewsPost == null)
            {
                return NotFound();
            }

            var newsPost = await _context.NewsPost
                .FirstOrDefaultAsync(m => m.News_Id == id);
            if (newsPost == null)
            {
                return NotFound();
            }

            return View(newsPost);
        }

        // POST: NewsPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NewsPost == null)
            {
                return Problem("Entity set 'DuHocContext.NewsPost'  is null.");
            }
            var newsPost = await _context.NewsPost.FindAsync(id);
            if (newsPost != null)
            {
                _context.NewsPost.Remove(newsPost);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsPostExists(int id)
        {
          return _context.NewsPost.Any(e => e.News_Id == id);
        }
    }
}
