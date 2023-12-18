using MediatR;

namespace WebLibrary.Logic.Commands.Books
{
    public record DeleteBookCommand(Guid id) : IRequest<Unit>;

}
