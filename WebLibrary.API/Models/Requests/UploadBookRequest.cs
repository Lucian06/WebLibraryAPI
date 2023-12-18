using Microsoft.AspNetCore.JsonPatch;
using WebLibrary.Logic.DTOs;

namespace WebLibrary.API.Models.Requests
{
    public record UploadBookRequest(Guid BookId, JsonPatchDocument<BookDto> Book);

}
