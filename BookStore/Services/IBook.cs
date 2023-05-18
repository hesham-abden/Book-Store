using BookStore.Models;

namespace BookStore.Services
{
    public interface IBook
    {
        void AddBook(Book book);
        Task  Delete(int bookid);
        Task<Book> GetBookbyId(int? id);
        Task<List<Book>>  GetBooks();
        void Update(Book book);
        public bool IsExists(int bookid);
        public  List<Author> GetAllAuthors();
        public void SaveChanges();
        public List<BookAuthors> GetAllBookAuthors();
        public List<Author> GetAuthorbyBook(Book book);
    }
}