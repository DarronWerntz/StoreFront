using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFront.DATA.EF.Models;
using StoreFront.UI.MVC.Utilities;
using System.Drawing;
using Microsoft.AspNetCore.Hosting;

namespace StoreFront.UI.MVC.Controllers
{
    public class FurnituresController : Controller
    {
        private readonly StoreFrontContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public FurnituresController(StoreFrontContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> Edit(int id, [Bind("FurnitureId,CategoryId,MaterialId,Name,Price,Description,Image,StockQuantity,IsDiscontinued, FurnitureImage")] Furniture furniture)
        {
            if (id != furniture.FurnitureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                #region EDIT File Upload
                //retain old image file name so we can delete if a new file was uploaded
                string oldImageName = furniture.Image;

                //Check if the user uploaded a file
                if (furniture.FurnitureImage != null)
                {
                    //get the file's extension
                    string ext = Path.GetExtension(furniture.FurnitureImage.FileName);

                    //list valid extensions
                    string[] validExts = { ".jpeg", ".jpg", ".png", ".gif" };

                    //check the file's extension against the list of valid extensions
                    if (validExts.Contains(ext.ToLower()) && furniture.FurnitureImage.Length < 4_194_303)
                    {
                        //generate a unique file name
                        furniture.Image = Guid.NewGuid() + ext;
                        //build our file path to save the image
                        string webRootPath = _webHostEnvironment.WebRootPath;
                        string fullPath = webRootPath + "/img/";

                        //Delete the old image
                        if (oldImageName != "noimage.png")
                        {
                            ImageUtility.Delete(fullPath, oldImageName);
                        }

                        //Save the new image to webroot
                        using (var memoryStream = new MemoryStream())
                        {
                            await furniture.FurnitureImage.CopyToAsync(memoryStream);
                            using (var img = Image.FromStream(memoryStream))
                            {
                                int maxImageSize = 500;
                                int maxThumbSize = 100;
                                ImageUtility.ResizeImage(fullPath, furniture.Image, img, maxImageSize, maxThumbSize);
                            }
                        }

                    }
                }
                #endregion

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
