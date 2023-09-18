using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudyPortal.Data;
using StudyPortal.Models;

namespace StudyPortal.Controllers
{
    public class NewsModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NewsModels
        public async Task<IActionResult> Index()
        {
              return _context.News != null ? 
                          View(await _context.News.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.News'  is null.");
        }

        // GET: NewsModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var newsModel = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsModel == null)
            {
                return NotFound();
            }

            return View(newsModel);
        }

        // GET: NewsModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NewsModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,Author,Published")] NewsModel newsModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newsModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newsModel);
        }

        // GET: NewsModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var newsModel = await _context.News.FindAsync(id);
            if (newsModel == null)
            {
                return NotFound();
            }
            return View(newsModel);
        }

        // POST: NewsModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,Author,Published")] NewsModel newsModel)
        {
            if (id != newsModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newsModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsModelExists(newsModel.Id))
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
            return View(newsModel);
        }

        // GET: NewsModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var newsModel = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsModel == null)
            {
                return NotFound();
            }

            return View(newsModel);
        }

        // POST: NewsModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.News == null)
            {
                return Problem("Entity set 'ApplicationDbContext.News'  is null.");
            }
            var newsModel = await _context.News.FindAsync(id);
            if (newsModel != null)
            {
                _context.News.Remove(newsModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsModelExists(int id)
        {
          return (_context.News?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
