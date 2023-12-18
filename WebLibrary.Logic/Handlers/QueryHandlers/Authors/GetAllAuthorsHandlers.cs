using AutoMapper;
using MediatR;
using WebLibrary.DAL.Models;
using WebLibrary.DAL.Services.Contracts;
using WebLibrary.Logic.DTOs;
using WebLibrary.Logic.Queries.Authors;

namespace WebLibrary.Logic.Handlers.QueryHandlers.Authors
{
    public class GetAllAuthorsHandlers : IRequestHandler<GetAllAuthorsQuery, IEnumerable<AuthorDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAuthorDalService _authorDalService;

        public GetAllAuthorsHandlers(IMapper mapper, IAuthorDalService authorDalService)
        {
            _authorDalService = authorDalService;
            _mapper = mapper;
        }

        async Task<IEnumerable<AuthorDto>> IRequestHandler<GetAllAuthorsQuery, IEnumerable<AuthorDto>>.Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var authors = await _authorDalService.GetAllAuthors();
                return _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorDto>>(authors);
            }
            catch (ArgumentNullException ex)
            {
                //do something
                throw new ArgumentException("Opps, something went wrong");
            }
            catch (Exception ex)
            {
                return new List<AuthorDto>();
            }
        }
    }
}
