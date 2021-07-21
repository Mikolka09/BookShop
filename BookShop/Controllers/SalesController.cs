using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookShop.Data;
using BookShop.Models;
using BookShop.ViewModels;

namespace BookShop.Controllers
{
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public List<Book> books;
        public List<Shops> shops;
        public List<Sales> sales;
        SelectModelBooksShops model;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public SelectModelBooksShops ModelsBase()
        {
            sales = _context.Sales.ToList();
            shops = _context.Shops.ToList();
            books = _context.Book.ToList();
            model = new SelectModelBooksShops { ShopsList = shops, BooksList = books, SalesList = sales };
            return model;
        }


        // GET: Sales

        public IActionResult Index()
        {
            return View(ModelsBase());
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sId = _context.Sales.FirstOrDefault(m => m.Id == id).ShopId;
            var bId = _context.Sales.FirstOrDefault(m => m.Id == id).BookId;
            ViewBag.Shop = _context.Shops.FirstOrDefault(m => m.Id == sId).Name;
            ViewBag.Book = _context.Book.FirstOrDefault(m => m.Id == bId).Name;
            var sales = await _context.Sales
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sales == null)
            {
                return NotFound();
            }

            return View(sales);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            ViewBag.Shops = new SelectList(_context.Shops, "Id", "Name");
            ViewBag.Books = new SelectList(_context.Book, "Id", "Name");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Price,Quantity,SaleDate,BookId,ShopId")] Sales sales)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sales);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sales);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Shops = new SelectList(_context.Shops, "Id", "Name");
            ViewBag.Books = new SelectList(_context.Book, "Id", "Name");
            var sales = await _context.Sales.FindAsync(id);
            if (sales == null)
            {
                return NotFound();
            }
            return View(sales);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Price,Quantity,SaleDate,BookId,ShopId")] Sales sales)
        {
            if (id != sales.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sales);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesExists(sales.Id))
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
            return View(sales);
        }

        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sales = await _context.Sales
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sales == null)
            {
                return NotFound();
            }

            return View(sales);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sales = await _context.Sales.FindAsync(id);
            _context.Sales.Remove(sales);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesExists(int id)
        {
            return _context.Sales.Any(e => e.Id == id);
        }
    }
}
