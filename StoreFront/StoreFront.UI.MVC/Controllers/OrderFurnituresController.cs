using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFront.DATA.EF.Models;

namespace StoreFront.UI.MVC.Controllers
{
    public class OrderFurnituresController : Controller
    {
        private readonly StoreFrontContext _context;

        public OrderFurnituresController(StoreFrontContext context)
        {
            _context = context;
        }

        // GET: OrderFurnitures
        public async Task<IActionResult> Index()
        {
            var storeFrontContext = _context.OrderFurnitures.Include(o => o.Furniture).Include(o => o.Order);
            return View(await storeFrontContext.ToListAsync());
        }

        // GET: OrderFurnitures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderFurnitures == null)
            {
                return NotFound();
            }

            var orderFurniture = await _context.OrderFurnitures
                .Include(o => o.Furniture)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.OrderFurniture1 == id);
            if (orderFurniture == null)
            {
                return NotFound();
            }

            return View(orderFurniture);
        }

        // GET: OrderFurnitures/Create
        public IActionResult Create()
        {
            ViewData["FurnitureId"] = new SelectList(_context.Furnitures, "FurnitureId", "Name");
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "UserId");
            return View();
        }

        // POST: OrderFurnitures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,FurnitureId,Quantity,Price,Discount,OrderFurniture1")] OrderFurniture orderFurniture)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderFurniture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FurnitureId"] = new SelectList(_context.Furnitures, "FurnitureId", "Name", orderFurniture.FurnitureId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "UserId", orderFurniture.OrderId);
            return View(orderFurniture);
        }

        // GET: OrderFurnitures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderFurnitures == null)
            {
                return NotFound();
            }

            var orderFurniture = await _context.OrderFurnitures.FindAsync(id);
            if (orderFurniture == null)
            {
                return NotFound();
            }
            ViewData["FurnitureId"] = new SelectList(_context.Furnitures, "FurnitureId", "Name", orderFurniture.FurnitureId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "UserId", orderFurniture.OrderId);
            return View(orderFurniture);
        }

        // POST: OrderFurnitures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,FurnitureId,Quantity,Price,Discount,OrderFurniture1")] OrderFurniture orderFurniture)
        {
            if (id != orderFurniture.OrderFurniture1)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderFurniture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderFurnitureExists(orderFurniture.OrderFurniture1))
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
            ViewData["FurnitureId"] = new SelectList(_context.Furnitures, "FurnitureId", "Name", orderFurniture.FurnitureId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "UserId", orderFurniture.OrderId);
            return View(orderFurniture);
        }

        // GET: OrderFurnitures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderFurnitures == null)
            {
                return NotFound();
            }

            var orderFurniture = await _context.OrderFurnitures
                .Include(o => o.Furniture)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.OrderFurniture1 == id);
            if (orderFurniture == null)
            {
                return NotFound();
            }

            return View(orderFurniture);
        }

        // POST: OrderFurnitures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderFurnitures == null)
            {
                return Problem("Entity set 'StoreFrontContext.OrderFurnitures'  is null.");
            }
            var orderFurniture = await _context.OrderFurnitures.FindAsync(id);
            if (orderFurniture != null)
            {
                _context.OrderFurnitures.Remove(orderFurniture);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderFurnitureExists(int id)
        {
          return (_context.OrderFurnitures?.Any(e => e.OrderFurniture1 == id)).GetValueOrDefault();
        }
    }
}
