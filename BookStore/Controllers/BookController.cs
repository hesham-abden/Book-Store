using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using BookStore.Services;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BookStore.Controllers
{
    [Authorize(Roles = "Admin,Instructor")]
    public class BookController : Controller
    {
        IBook _context;
        public BookController(IBook context)
        {

            _context = context;
        }
        public async Task<IActionResult> AddAuthor(int id)
        {
            var book = await _context.GetBookbyId(id);
            var Authors = _context.GetAuthorbyBook(book);
            var tempNotAuthors = _context.GetAllAuthors().ToList();
            var NotAuthors = tempNotAuthors.Except(Authors).ToList();
            ViewBag.Authors = new SelectList(Authors, "Id","Name");
            ViewBag.NotAuthors = new SelectList(NotAuthors,"Id","Name");
            
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> AddAuthor(int id, int[] Authors, int[] NotAuthors,int Order)
        {
            var book= await _context.GetBookbyId(id);
            foreach (var item in NotAuthors)
            {
                book.BookAuthors.Add(new BookAuthors { AuthorId = item,BookId=id,Order=Order });
            }
            foreach (var item in Authors)
            {
               var tempauthor= _context.GetAllBookAuthors().Find(a => a.AuthorId == item && a.BookId == id);
                book.BookAuthors.Remove(tempauthor);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
            
        }
        public IActionResult AddOrder(int id)
        {
            var book =  _context.GetBookbyId(id).Result;
            var Authors = _context.GetAuthorbyBook(book);
            ViewBag.Book = book;
            
            ViewBag.Authors=new SelectList(Authors,"Id","Name");
            return View();
        }
        [HttpPost]
        public IActionResult AddOrder(int id,int Authors,int Order)
        {
            var bookauthors=_context.GetAllBookAuthors().FirstOrDefault(a=>a.AuthorId==Authors&&a.BookId==id);
            if (bookauthors != null)
            { bookauthors.Order = Order; _context.SaveChanges(); }
            return RedirectToAction(nameof(Index));
        }
        // GET: Book
        [AllowAnonymous]
        public  async Task<IActionResult> Index()
        {
            
            return View(await _context.GetBooks());
        }

        // GET: Book/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || await _context.GetBooks() == null)
            {
                return NotFound();
            }

            var book = await _context.GetBookbyId(id);
                
            if (book == null)
            {
                return NotFound();
            }
            ViewBag.Authors = _context.GetAllBookAuthors().FindAll(a => a.BookId == id).ToList();
            return View(book);
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Price,Publisher,PublishedOn,ImageUrl")] Book book,IFormFile img)
        {
            if (ModelState.IsValid)
            {
                _context.AddBook(book);
                if (img != null)
                {

                    string imgname = book.Id.ToString() + "." + img.FileName.Split(".").Last();
                    using (var obj = new FileStream(@".\wwwroot\images\" + imgname, FileMode.Create))
                    {
                        await img.CopyToAsync(obj);
                        book.ImgName = imgname;
                        _context.SaveChanges();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }
        public IActionResult Download()
        {
            return File("images/1002.png", "image/x-png", "9545.png");
        }

        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || await _context.GetBooks() == null)
            {
                return NotFound();
            }

            var book = await _context.GetBookbyId(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(int id, [Bind("Id,Title,Description,Price,Publisher,PublishedOn,ImageUrl")] Book book)
        {
            if (id != book.Id)
            {
                return   NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    
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

        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null ||  await _context.GetBooks() == null)
            {
                return NotFound();
            }

            var book = await _context.GetBookbyId(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _context.GetBooks() == null)
            {
                return Problem("Entity set 'BookDbContext.Books'  is null.");
            }
            var book = await _context.GetBookbyId(id);
            if (book != null)
            {
                await _context.Delete(id);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.IsExists(id);
        }
    }
}
