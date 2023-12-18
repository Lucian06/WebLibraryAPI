using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Net;
using WebLibrary.DAL.Models;
using WebLibrary.DAL.Services.Contracts;

namespace WebLibrary.DAL.Services
{
    public class BookDalService : IBookDalService
    {
        private readonly string? _dbConnection;
        private readonly MainDbContext _dbContext;

        public BookDalService(IConfiguration configuration, MainDbContext dbContext)
        {
            string connectionName = configuration.GetSection("ConnectionString").Value.ToString();

            _dbConnection = Environment.GetEnvironmentVariable(connectionName);

            if (string.IsNullOrEmpty(_dbConnection))
                throw new ArgumentNullException($"Connection string is not set. Please check variable with name: {connectionName}"); //add logging

            _dbContext = dbContext;
        }

        /// <summary>
        /// Remove one book from the database
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        /// <exception cref="NpgsqlException"></exception>
        public async Task DeleteBook(Guid bookId)
        {
            try
            {
                using var connection = new NpgsqlConnection(_dbConnection);
                connection.Open();
                using var transaction = connection.BeginTransaction();
                try
                {
                    await connection.ExecuteAsync("DELETE FROM \"Main\".\"Books\" where \"Id\" = @BookId", new { BookId = bookId }, transaction);
                    await connection.ExecuteAsync("DELETE FROM \"Main\".\"AuthorBook\" where \"BooksId\" = @BookId", new { BookId = bookId }, transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new NpgsqlException("Something went wrong removing the data.");
                }
            }
            catch (Exception e)
            {
                throw new NpgsqlException("Something went wrong connecting to the database.");
            }
        }

        /// <summary>
        /// Get all books from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NpgsqlException"></exception>
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            try
            {
                using var connection = new NpgsqlConnection(_dbConnection);
                var query = "SELECT b.*, a.\"Name\" FROM \"Main\".\"Books\" b INNER JOIN \"Main\".\"AuthorBook\" ab on b.\"Id\" = ab.\"BooksId\" INNER JOIN \"Main\".\"Authors\" a on a.\"Id\" = ab.\"AuthorsId\"";
                return await connection.QueryAsync<Book>(query);
            }
            catch (Exception ex)
            {
                throw new NpgsqlException("Something went wrong when retriving the data.");
            }
        }

        /// <summary>
        /// Retrive one book by Id
        /// </summary>
        /// <param name="BookId"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="NpgsqlException"></exception>
        public async Task<Book> GetBookById(Guid bookId)
        {
            try
            {
                using var connection = new NpgsqlConnection(_dbConnection);
                var query = "SELECT b.*, a.\"Name\" FROM \"Main\".\"Books\" b INNER JOIN \"Main\".\"AuthorBook\" ab on b.\"Id\" = ab.\"BooksId\" INNER JOIN \"Main\".\"Authors\" a on a.\"Id\" = ab.\"AuthorsId\" WHERE b.\"Id\" = @Id;";
                var dbBook = await connection.QueryFirstOrDefaultAsync<Book>(query, new { Id = bookId });
                if (dbBook is null)
                    throw new KeyNotFoundException($"Book id nout fount. Id: {bookId}");

                return dbBook;
            }
            catch (Exception ex)
            {
                throw new NpgsqlException("Something went wrong when retriving the data.");
            }
        }


        /// <summary>
        /// Insert one book into the database
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        /// <exception cref="NpgsqlException"></exception>
        public async Task<Book> InserBook(Book book)
        {
            try
            {
                using var connection = new NpgsqlConnection(_dbConnection);
                connection.Open();
                using var transaction = connection.BeginTransaction();
                try
                {

                    var newBookId = await connection.QuerySingleAsync<Guid>("INSERT INTO \"Main\".\"Books\" (\"Title\", \"Cover\") VALUES(@BookTitle, @BookCover) RETURNING \"Id\";", new { BookTitle = book.Title, BookCover = book.Cover }, transaction);
                    book.Id = newBookId;

                    foreach (var author in book.Authors)
                        await connection.ExecuteAsync("INSERT INTO \"Main\".\"AuthorBook\" (\"AuthorsId\", \"BooksId\") VALUES(@AuthorsId, @BooksId);", new { AuthorsId = author.Id, BooksId = newBookId }, transaction);


                    transaction.Commit();

                    return book;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(); // ensure atomicity

                    //log ex first;
                    throw new NpgsqlException("Something went wrong inserting the data.");
                }
            }
            catch (Exception e)
            {
                throw new NpgsqlException("Something went wrong connecting to the database.");
            }
        }

        /// <summary>
        /// Update the book entity
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        /// <exception cref="NpgsqlException"></exception>
        public async Task UpdateBook(Book book)
        {
            try
            {
                var query = "UPDATE \"Main\".\"Books\" SET \"Title\"=@NewTitle, \"Cover\"=@NewCover WHERE \"Id\"=@BookId;";

                using var connection = new NpgsqlConnection(_dbConnection);
                var dbBook = await connection.ExecuteAsync(query, new { NewTitle = book.Title, NewCover = book.Cover, BookId = book.Id });
            }
            catch (Exception ex)
            {
                //log ex first;
                throw new NpgsqlException("Something went wrong updateing the data.");
            }
        }
    }
}
