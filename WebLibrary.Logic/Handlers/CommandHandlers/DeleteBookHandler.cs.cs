using MediatR;
using WebLibrary.DAL.Services.Contracts;
using WebLibrary.Logic.Commands.Books;

namespace WebLibrary.Logic.Handlers.CommandHandlers
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, Unit>
    {
        private readonly IBookDalService _bookDalService;

        public DeleteBookHandler(IBookDalService bookDalService)
        {
            _bookDalService = bookDalService;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            await _bookDalService.DeleteBook(request.id);
            return Unit.Value;
        }
    }
}
