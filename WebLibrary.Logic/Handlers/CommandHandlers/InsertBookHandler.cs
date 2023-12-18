using AutoMapper;
using MediatR;
using WebLibrary.DAL.Models;
using WebLibrary.DAL.Services.Contracts;
using WebLibrary.Logic.Commands.Books;
using WebLibrary.Logic.DTOs;

namespace WebLibrary.Logic.Handlers.CommandHandlers
{
    public class InsertBookHandler : IRequestHandler<InsertBookCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IBookDalService _bookDalService;

        public InsertBookHandler(IBookDalService bookDalService, IMapper mapper)
        {
            _bookDalService = bookDalService;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(InsertBookCommand request, CancellationToken cancellationToken)
        {
            var toAdd = _mapper.Map<BookDto, Book>(request.NewBook);

            var newBook = await _bookDalService.InserBook(toAdd);
            return newBook.Id;
        }
    }
}
