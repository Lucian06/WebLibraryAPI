using MediatR;
using WebLibrary.Logic.DTOs;

namespace WebLibrary.Logic.Queries.Books
{
    public record GetBookByIdQuery(Guid BookId) : IRequest<BookDto>;
}
