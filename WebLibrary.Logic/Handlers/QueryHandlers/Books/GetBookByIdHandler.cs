using AutoMapper;
using MediatR;
using WebLibrary.DAL.Models;
using WebLibrary.DAL.Services.Contracts;
using WebLibrary.Logic.DTOs;
using WebLibrary.Logic.Queries.Books;

namespace WebLibrary.Logic.Handlers.QueryHandlers.Books
{
    public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, BookDto>
    {
        private readonly IMapper _mapper;
        private readonly IBookDalService _bookDalService;

        public GetBookByIdHandler(IMapper mapper, IBookDalService bookDalService)
        {
            _mapper = mapper;
            _bookDalService = bookDalService;
        }

        public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookDalService.GetBookById(request.BookId);
            return _mapper.Map<Book, BookDto>(book);
        }
    }
}
