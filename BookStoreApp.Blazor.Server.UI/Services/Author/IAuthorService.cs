using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services.Author
{
    public interface IAuthorService
    {
        Task<Response<List<AuthorReadOnlyDto>>> GetAuthors();
        Task<Response<int>> Create(AuthorCreateDto authorCreateDto);
        Task<Response<int>> Delete(int id);
        Task<Response<int>> Update(int id, AuthorUpdateDto author);
        Task<Response<AuthorDetailsDto>> GetAuthor(int id);
        Task<Response<AuthorUpdateDto>> GetForUpdate(int id);

    }
}