using practise.Entitys;

namespace practise.Services
{
    public interface IBookService
    {
        void AddBook(Book book);

        void UpdateBook(Book book);


        void DeleteBook(int bookId);


        Book GetBookById(int bookId);


        List<Book> GetAllBooks();

        List<Book> GetBookByName(string search);
    }
}
