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
    public class ShopsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public List<Countries> countries;
        public List<Shops> shops;
        SelectModelCoutryShops model;

        public ShopsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public SelectModelCoutryShops ModelsCountry()
        {
            countries = new List<Countries>(_context.Countries.ToList());
            shops = new List<Shops>(_context.Shops.ToList());
            model = new SelectModelCoutryShops { CountryList = countries, ShopsList = shops };
            return model;
        }

        // GET: Shops
        public IActionResult Index()
        {
           
            return View(ModelsCountry());
        }

        // GET: Shops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int sId = _context.Shops.FirstOrDefault(m => m.Id == id).CountryId;
            ViewBag.Country = _context.Countries.FirstOrDefault(m => m.Id == sId).Name;
            var shops = await _context.Shops
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shops == null)
            {
                return NotFound();
            }

            return View(shops);
        }

        // GET: Shops/Create
        public IActionResult Create()
        {
            ViewBag.Countries = new SelectList(_context.Countries, "Id", "Name");
            return View();
        }

        // POST: Shops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CountryId")] Shops shops)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shops);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shops);
        }

        // GET: Shops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Countries = new SelectList(_context.Countries, "Id", "Name");
            var shops = await _context.Shops.FindAsync(id);
            if (shops == null)
            {
                return NotFound();
            }
            return View(shops);
        }

        // POST: Shops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CountryId")] Shops shops)
        {
            if (id != shops.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shops);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShopsExists(shops.Id))
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
            return View(shops);
        }

        // GET: Shops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shops = await _context.Shops
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shops == null)
            {
                return NotFound();
            }

            return View(shops);
        }

        // POST: Shops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shops = await _context.Shops.FindAsync(id);
            _context.Shops.Remove(shops);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShopsExists(int id)
        {
            return _context.Shops.Any(e => e.Id == id);
        }
    }
}
