using MediatR;
using WebLibrary.Logic.DTOs;

namespace WebLibrary.Logic.Queries.Books
{
    public record GetAllBooksQuery() : IRequest<List<BookDto>>;
}
