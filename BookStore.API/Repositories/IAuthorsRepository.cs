using BookStore.API.Data;
using BookStore.API.Models.Author;

namespace BookStore.API.Repositories
{
    public interface IAuthorsRepository: IGenericRepository<Author>
    {
        Task<AuthorDetailsDto> GetAuthorDetailsAsync(int id);
    }
}
