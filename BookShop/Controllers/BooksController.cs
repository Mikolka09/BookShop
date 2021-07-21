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
using System.Text.Json;

namespace BookShop.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        public List<Author> authors;
        public List<Themes> themes;
        public List<Book> books;
        SelectModelAuthorsThemes model;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        public SelectModelAuthorsThemes ModelsBase()
        {
            authors = _context.Author.ToList();
            themes = _context.Themes.ToList();
            books = _context.Book.ToList();
                       
            model = new SelectModelAuthorsThemes { AuthorList = authors, ThemeList = themes, BookList = books };
            return model;
        }

        // GET: Books
        public ActionResult Index()
        {
            return View(ModelsBase());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tId = _context.Book.FirstOrDefault(m => m.Id == id).ThemeId;
            var aId = _context.Book.FirstOrDefault(m => m.Id == id).AuthorId;
            ViewBag.Author = _context.Author.FirstOrDefault(m => m.Id == aId).Name;
            ViewBag.Theme = _context.Themes.FirstOrDefault(m => m.Id == tId).Name;
            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewBag.Themes = new SelectList(_context.Themes, "Id", "Name");
            ViewBag.Authors = new SelectList(_context.Author, "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ShortDesc,Pages,Codes,Price,PublishDate,AuthorId,ThemeId")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Themes = new SelectList(_context.Themes, "Id", "Name");
            ViewBag.Authors = new SelectList(_context.Author, "Id", "Name");
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ShortDesc,Pages,Codes,Price,PublishDate,AuthorId,ThemeId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var codes = JsonSerializer.Deserialize<HashSet<string>>(book.Codes.First(), null);
                book.Codes.Clear();
                foreach (var item in codes)
                {
                    book.Codes.Add(item);
                }

                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tId = _context.Book.FirstOrDefault(m => m.Id == id).ThemeId;
            var aId = _context.Book.FirstOrDefault(m => m.Id == id).AuthorId;
            ViewBag.Author = _context.Author.FirstOrDefault(m => m.Id == aId).Name;
            ViewBag.Theme = _context.Themes.FirstOrDefault(m => m.Id == tId).Name;
            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
