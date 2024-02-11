using Microsoft.EntityFrameworkCore;
using practise.Database;
using practise.Entitys;

namespace practise.Services
{
    public class BookService : IBookService
    {

        private readonly BookContext Context = null;
        public BookService(BookContext context)
        {

            this.Context = context;

        }
        public void AddBook(Book book)
        {
            Context.Books.Add(book);
            Context.SaveChanges();
        }

        public void DeleteBook(int bookId)
        {
            var bookToDelete = Context.Books.SingleOrDefault(c => c.BookId == bookId);
            if (bookToDelete != null)
            {
                Context.Books.Remove(bookToDelete);
                Context.SaveChanges();

            }
        }

        public List<Book> GetAllBooks()
        {
            var res = Context.Books.Include(p => p.Usernew).ToList();
            return res;
        }

        public Book GetBookById(int bookId)
        {
            return Context.Books.Find(bookId);
        }

        public List<Book> GetBookByName(string search)
        {
            var res = Context.Books
        .Include(p => p.Usernew)
        
        .AsEnumerable()
        .Where(p => string.IsNullOrEmpty(search) || p.Title.Contains(search, StringComparison.OrdinalIgnoreCase))
        .ToList();

            return res;
        }

        public void UpdateBook(Book book)
        {
            if (book != null)
            {
                Context.Books.Update(book);
                Context.SaveChanges();

            }
        }
    }

}