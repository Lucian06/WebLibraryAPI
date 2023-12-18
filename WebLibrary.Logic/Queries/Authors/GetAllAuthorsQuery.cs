using MediatR;
using WebLibrary.Logic.DTOs;

namespace WebLibrary.Logic.Queries.Authors
{
    public record GetAllAuthorsQuery() : IRequest<IEnumerable<AuthorDto>>;
}
