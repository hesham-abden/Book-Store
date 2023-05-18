using BookStore.Services;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Models
{
    public class BookDB : IBook
    {
        BookDbContext db;
        public BookDB(BookDbContext _db)
        {
            db = _db;
        }

        public async Task<List<Book>> GetBooks()
        {
            return  await db.Books.ToListAsync();
        }
        public void AddBook(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
        }
        public async Task<Book> GetBookbyId(int? id)
        {
            return await db.Books.FirstOrDefaultAsync(a => a.Id == id);
        }
        public  void Update(Book book)
        {
            Book old =  db.Books.FirstOrDefault(a => a.Id == book.Id);
            old.Title = book.Title;
            old.Publisher = book.Publisher;
            old.Description = book.Description;
             db.SaveChanges();
        }
        public async Task Delete(int bookid)
        {
            Book old = await db.Books.FirstOrDefaultAsync(a => a.Id == bookid);
            db.Books.Remove(old);
            await db.SaveChangesAsync();
        }
        public bool IsExists(int bookid) 
        {
            return db.Books.Any(d => d.Id == bookid);
        }
        public  List<Author> GetAllAuthors()
        {
            return  db.Authors.ToList();
        }
        public void SaveChanges()
        {
            db.SaveChanges();
            
        }
        public List<BookAuthors> GetAllBookAuthors()
        {
            return db.BookAuthors.Include(a=>a.Author).ToList();
        }
        public List<Author> GetAuthorbyBook(Book book)
        {
            var authorbooks = db.BookAuthors.Where(b => b.BookId == book.Id).Select(a=>a.Author).ToList();
            return authorbooks;
        }
    }
}
