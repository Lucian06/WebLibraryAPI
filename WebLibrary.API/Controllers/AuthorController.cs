using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.Logic.DTOs;
using WebLibrary.Logic.Queries.Authors;

namespace WebLibrary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAllAuthors()
        {
            try
            {
                return Ok(await _mediator.Send<IEnumerable<AuthorDto>>(new GetAllAuthorsQuery()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
