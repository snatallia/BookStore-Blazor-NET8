using BookStoreApp.Blazor.WebAssembly.UI.Services.Base;

namespace BookStoreApp.Blazor.WebAssembly.UI.Services.Book
{
    public interface IBookService
    {
        Task<Response<List<BookReadOnlyDto>>> GetBooks();
        Task<Response<int>> Create(BookCreateDto authorCreateDto);
        Task<Response<int>> Delete(int id);
        Task<Response<int>> Update(int id, BookUpdateDto author);
        Task<Response<BookDetailsDto>> GetBook(int id);
        Task<Response<BookUpdateDto>> GetForUpdate(int id);
    }
}
