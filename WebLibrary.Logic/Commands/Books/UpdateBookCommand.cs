using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using WebLibrary.Logic.DTOs;

namespace WebLibrary.Logic.Commands.Books
{
    public record UpdateBookCommand(Guid BookId, JsonPatchDocument<BookDto> Book) : IRequest<Unit>;

}
