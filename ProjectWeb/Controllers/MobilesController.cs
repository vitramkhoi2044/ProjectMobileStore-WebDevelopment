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
    public class MobilesController : Controller
    {
        private readonly AppDbContext _context;

        public MobilesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Mobiles
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Mobiles.Include(m => m.BrandCategories);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Mobiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mobiles == null)
            {
                return NotFound();
            }

            var mobiles = await _context.Mobiles
                .Include(m => m.BrandCategories)
                .FirstOrDefaultAsync(m => m.MobileID == id);
            if (mobiles == null)
            {
                return NotFound();
            }

            return View(mobiles);
        }

        // GET: Mobiles/Create
        public IActionResult Create()
        {
            ViewData["BrandID"] = new SelectList(_context.BrandCategories, "BrandID", "BrandID");
            return View();
        }

        // POST: Mobiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MobileID,MobileName,Color,MobileDescription,Screen,Ram,Rom,Chip,FrontCamera,BackCamera,Battery,Charger,CreateDate,TotalImage,MobileQuantity,MobileType,Price,BrandID")] Mobiles mobiles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mobiles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandID"] = new SelectList(_context.BrandCategories, "BrandID", "BrandID", mobiles.BrandID);
            return View(mobiles);
        }

        // GET: Mobiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mobiles == null)
            {
                return NotFound();
            }

            var mobiles = await _context.Mobiles.FindAsync(id);
            if (mobiles == null)
            {
                return NotFound();
            }
            ViewData["BrandID"] = new SelectList(_context.BrandCategories, "BrandID", "BrandID", mobiles.BrandID);
            return View(mobiles);
        }

        // POST: Mobiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MobileID,MobileName,Color,MobileDescription,Screen,Ram,Rom,Chip,FrontCamera,BackCamera,Battery,Charger,CreateDate,TotalImage,MobileQuantity,MobileType,Price,BrandID")] Mobiles mobiles)
        {
            mobiles.CreateDate = DateTime.Parse(DateTime.Parse(mobiles.CreateDate.ToString()).ToShortDateString());
            if (id != mobiles.MobileID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mobiles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MobilesExists(mobiles.MobileID))
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
            ViewData["BrandID"] = new SelectList(_context.BrandCategories, "BrandID", "BrandID", mobiles.BrandID);
            return View(mobiles);
        }

        // GET: Mobiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mobiles == null)
            {
                return NotFound();
            }

            var mobiles = await _context.Mobiles
                .Include(m => m.BrandCategories)
                .FirstOrDefaultAsync(m => m.MobileID == id);
            if (mobiles == null)
            {
                return NotFound();
            }

            return View(mobiles);
        }

        // POST: Mobiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mobiles == null)
            {
                return Problem("Entity set 'AppDbContext.Mobiles'  is null.");
            }
            var mobiles = await _context.Mobiles.FindAsync(id);
            if (mobiles != null)
            {
                _context.Mobiles.Remove(mobiles);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MobilesExists(int id)
        {
          return _context.Mobiles.Any(e => e.MobileID == id);
        }
    }
}
