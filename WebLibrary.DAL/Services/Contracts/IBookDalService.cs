using WebLibrary.DAL.Models;

namespace WebLibrary.DAL.Services.Contracts
{
    public interface IBookDalService
    {
        public Task<IEnumerable<Book>> GetAllBooks();
        public Task<Book> GetBookById(Guid BookId);
        public Task<Book> InserBook(Book book);
        public Task UpdateBook(Book book);
        public Task DeleteBook(Guid BookId);
    }
}
