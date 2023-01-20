using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectWeb.Data;

namespace ProjectWeb.Controllers
{
    public class BrandCategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public BrandCategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: BrandCategories
        public async Task<IActionResult> Index()
        {
              return View(await _context.BrandCategories.ToListAsync());
        }

        // GET: BrandCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BrandCategories == null)
            {
                return NotFound();
            }

            var brandCategories = await _context.BrandCategories
                .FirstOrDefaultAsync(m => m.BrandID == id);
            if (brandCategories == null)
            {
                return NotFound();
            }

            return View(brandCategories);
        }

        // GET: BrandCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BrandCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BrandID,BrandName,BrandDescription")] BrandCategories brandCategories)
        {
            if (ModelState.IsValid)
            {
                _context.Add(brandCategories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brandCategories);
        }

        // GET: BrandCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BrandCategories == null)
            {
                return NotFound();
            }

            var brandCategories = await _context.BrandCategories.FindAsync(id);
            if (brandCategories == null)
            {
                return NotFound();
            }
            return View(brandCategories);
        }

        // POST: BrandCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BrandID,BrandName,BrandDescription")] BrandCategories brandCategories)
        {
            if (id != brandCategories.BrandID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brandCategories);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandCategoriesExists(brandCategories.BrandID))
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
            return View(brandCategories);
        }

        // GET: BrandCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BrandCategories == null)
            {
                return NotFound();
            }

            var brandCategories = await _context.BrandCategories
                .FirstOrDefaultAsync(m => m.BrandID == id);
            if (brandCategories == null)
            {
                return NotFound();
            }

            return View(brandCategories);
        }

        // POST: BrandCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BrandCategories == null)
            {
                return Problem("Entity set 'AppDbContext.BrandCategories'  is null.");
            }
            var brandCategories = await _context.BrandCategories.FindAsync(id);
            if (brandCategories != null)
            {
                _context.BrandCategories.Remove(brandCategories);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandCategoriesExists(int id)
        {
          return _context.BrandCategories.Any(e => e.BrandID == id);
        }
    }
}
