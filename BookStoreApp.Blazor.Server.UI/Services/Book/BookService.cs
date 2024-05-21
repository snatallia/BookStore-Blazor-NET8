using AutoMapper;
using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services.Book
{
    public class BookService : BaseHttpService, IBookService
    {
        private readonly IClient client;
        private readonly IMapper mapper;

        public BookService(IClient client, ILocalStorageService localStorage, IMapper mapper) : base(client, localStorage)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public async Task<Response<int>> Create(BookCreateDto bookCreateDto)
        {
            Response<int> response;
            try
            {
                await GetBearerTokenAsync();
                var author = await client.BooksPOSTAsync(bookCreateDto);
                response = new Response<int>
                {
                    Data = author.Id,
                    Success = true
                };
            }
            catch (ApiException ex)
            {

                response = ConvertApiExceptions<int>(ex);
            }
            return response;
        }

        public async Task<Response<int>> Delete(int id)
        {
            Response<int> response = new();
            try
            {
                await GetBearerTokenAsync();
                await client.BooksDELETEAsync(id);
                response = new Response<int>
                {
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<int>(ex);
            }
            return response;
        }

        public async Task<Response<BookDetailsDto>> GetBook(int id)
        {
            Response<BookDetailsDto> response;
            try
            {
                await GetBearerTokenAsync();
                var book = await client.BooksGETAsync(id);
                response = new Response<BookDetailsDto>
                {
                    Data = book,
                    Success = true
                };

            }
            catch (ApiException ex)
            {

                response = ConvertApiExceptions<BookDetailsDto>(ex);
            }
            return response;
        }

        public async Task<Response<List<BookReadOnlyDto>>> GetBooks()
        {
            Response<List<BookReadOnlyDto>> response;
            try
            {
                await GetBearerTokenAsync();
                var books = await client.BooksAllAsync();
                response = new Response<List<BookReadOnlyDto>>
                {
                    Data = books.ToList(),
                    Success = true
                };

            }
            catch (ApiException ex)
            {

                response = ConvertApiExceptions<List<BookReadOnlyDto>>(ex);
            }
            return response;
        }

        public async Task<Response<BookUpdateDto>> GetForUpdate(int id)
        {
            Response<BookUpdateDto> response;
            try
            {
                await GetBearerTokenAsync();
                var book = await client.BooksGETAsync(id);
                response = new Response<BookUpdateDto>
                {
                    Data = mapper.Map<BookUpdateDto>(book),
                    Success = true
                };
            }
            catch (ApiException ex)
            {

                response = ConvertApiExceptions<BookUpdateDto>(ex);
            }
            return response;
        }

        public async Task<Response<int>> Update(int id, BookUpdateDto book)
        {
            Response<int> response = new();
            try
            {
                await GetBearerTokenAsync();
                await client.BooksPUTAsync(id, book);
                response = new Response<int>
                {
                    Success = true
                };
            }
            catch (ApiException ex)
            {
                response = ConvertApiExceptions<int>(ex);
            }
            return response;
        }
    }
}
