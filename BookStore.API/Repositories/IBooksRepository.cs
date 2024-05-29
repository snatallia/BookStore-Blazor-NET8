using BookStore.API.Data;
using BookStore.API.Models.Book;

namespace BookStore.API.Repositories
{
    public interface IBooksRepository : IGenericRepository<Book>
    {
        Task<List<BookReadOnlyDto>> GetAllBooksAsync();
        Task<BookDetailsDto> GetBookAsync(int id);  
    }
}
