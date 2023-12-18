using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebLibrary.API.Models.Requests;
using WebLibrary.Logic.Commands.Books;
using WebLibrary.Logic.DTOs;
using WebLibrary.Logic.Queries.Books;

namespace WebLibrary.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<BookDto>> GetBook([FromQuery] Guid bookId)
        {
            try
            {
                return Ok(await _mediator.Send<BookDto>(new GetBookByIdQuery(bookId)));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> GetAllBooks()
        {
            return Ok(await _mediator.Send<List<BookDto>>(new GetAllBooksQuery()));
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> AddNewBook([FromBody] BookDto book)
        {
            try
            {
                var newBookId = await _mediator.Send<Guid>(new InsertBookCommand(book));
                return Created(newBookId.ToString(), null);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] JsonPatchDocument<BookDto> book)
        {
            if (id == Guid.Empty || book.Operations.Count() == 0)
                return BadRequest();

            try
            {
                await _mediator.Send<Unit>(new UpdateBookCommand(id, book));
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            try
            {
                await _mediator.Send<Unit>(new DeleteBookCommand(id));
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
    }
}
