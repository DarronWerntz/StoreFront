using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFront.DATA.EF.Models;
//using StoreFront.UI.MVC.Utilities;            *************************for picture uploader later
using System.Drawing;

namespace StoreFront.UI.MVC.Controllers
{
    public class FurnituresController : Controller
    {
        private readonly StoreFrontContext _context;

        public FurnituresController(StoreFrontContext context)
        {
            _context = context;
        }

        // GET: Furnitures
        public async Task<IActionResult> Index()
        {
            var storeFrontContext = _context.Furnitures.Include(f => f.Category).Include(f => f.Material);
            return View(await storeFrontContext.ToListAsync());
        }

        // GET: Furnitures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Furnitures == null)
            {
                return NotFound();
            }

            var furniture = await _context.Furnitures
                .Include(f => f.Category)
                .Include(f => f.Material)
                .FirstOrDefaultAsync(m => m.FurnitureId == id);
            if (furniture == null)
            {
                return NotFound();
            }

            return View(furniture);
        }

        // GET: Furnitures/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            ViewData["MaterialId"] = new SelectList(_context.Materials, "MaterialId", "MaterialId");
            return View();
        }

        // POST: Furnitures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FurnitureId,CategoryId,MaterialId,Name,Price,Description,Image,StockQuantity,IsDiscontinued")] Furniture furniture)
        {
            if (ModelState.IsValid)
            {
                _context.Add(furniture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", furniture.CategoryId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "MaterialId", "MaterialId", furniture.MaterialId);
            return View(furniture);
        }

        // GET: Furnitures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Furnitures == null)
            {
                return NotFound();
            }

            var furniture = await _context.Furnitures.FindAsync(id);
            if (furniture == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", furniture.CategoryId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "MaterialId", "MaterialId", furniture.MaterialId);
            return View(furniture);
        }

        // POST: Furnitures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FurnitureId,CategoryId,MaterialId,Name,Price,Description,Image,StockQuantity,IsDiscontinued")] Furniture furniture)
        {
            if (id != furniture.FurnitureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(furniture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FurnitureExists(furniture.FurnitureId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", furniture.CategoryId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "MaterialId", "MaterialId", furniture.MaterialId);
            return View(furniture);
        }

        // GET: Furnitures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Furnitures == null)
            {
                return NotFound();
            }

            var furniture = await _context.Furnitures
                .Include(f => f.Category)
                .Include(f => f.Material)
                .FirstOrDefaultAsync(m => m.FurnitureId == id);
            if (furniture == null)
            {
                return NotFound();
            }

            return View(furniture);
        }

        // POST: Furnitures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Furnitures == null)
            {
                return Problem("Entity set 'StoreFrontContext.Furnitures'  is null.");
            }
            var furniture = await _context.Furnitures.FindAsync(id);
            if (furniture != null)
            {
                _context.Furnitures.Remove(furniture);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FurnitureExists(int id)
        {
          return (_context.Furnitures?.Any(e => e.FurnitureId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> TiledProducts()
        {
            var products = _context.Furnitures.Where(p => (bool)!p.IsDiscontinued)
                .Include(p => p.Category).Include(p => p.Reviews).Include(p => p.Material).Include(p => p.OrderFurnitures);
            return View(await products.ToListAsync());
        }

    }
}
