using WebLibrary.DAL.Models;

namespace WebLibrary.DAL.Services.Contracts
{
    public interface  IAuthorDalService
    {
        Task<IEnumerable<Author>> GetAllAuthors();
    }
}
