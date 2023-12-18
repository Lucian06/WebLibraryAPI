using AutoMapper;
using MediatR;
using WebLibrary.DAL.Models;
using WebLibrary.DAL.Services.Contracts;
using WebLibrary.Logic.DTOs;
using WebLibrary.Logic.Queries.Books;

namespace WebLibrary.Logic.Handlers.QueryHandlers.Books
{
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, List<BookDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBookDalService _bookDalService;

        public GetAllBooksHandler(IBookDalService bookDalService, IMapper mapper)
        {
            _bookDalService = bookDalService;
            _mapper = mapper;
        }

        public async Task<List<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookDalService.GetAllBooks();
            return _mapper.Map<IEnumerable<Book>, List<BookDto>>(book);
        }
    }
}
