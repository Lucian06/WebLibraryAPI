using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using System.Net;
using WebLibrary.DAL.Models;
using WebLibrary.DAL.Services.Contracts;
using WebLibrary.Logic.Commands.Books;
using WebLibrary.Logic.DTOs;
using WebLibrary.Logic.Queries.Books;

namespace WebLibrary.Logic.Handlers.CommandHandlers
{
    public record UpdateBookHandler : IRequestHandler<UpdateBookCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IBookDalService _bookDalService;

        public UpdateBookHandler(IBookDalService bookDalService, IMapper mapper, IMediator mediator)
        {
            _bookDalService = bookDalService;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {

            var book = await _mediator.Send<BookDto>(new GetBookByIdQuery(request.BookId));
            if (book is null)
                throw new(nameof(book));

            request.Book.ApplyTo(book);

            var updatedBook = _mapper.Map<BookDto, Book>(book);

            await _bookDalService.UpdateBook(updatedBook);

            return Unit.Value;
        }
    }
}
