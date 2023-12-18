using MediatR;
using WebLibrary.DAL.Models;
using WebLibrary.Logic.DTOs;

namespace WebLibrary.Logic.Commands.Books
{
    public record InsertBookCommand(BookDto NewBook) : IRequest<Guid>;
}
