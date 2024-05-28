using AutoMapper;
using Blazored.LocalStorage;
using BookStoreApp.Blazor.WebAssembly.UI.Services.Base;
using System.Collections.Generic;

namespace BookStoreApp.Blazor.WebAssembly.UI.Services.Author
{
    public class AuthorService: BaseHttpService, IAuthorService
    {
        private readonly IClient client;
        private readonly IMapper mapper;

        public AuthorService(IClient client, ILocalStorageService localStorage, IMapper mapper): base(client, localStorage)
        {
            this.client = client;
            this.mapper = mapper;
        }

        public async Task<Response<List<AuthorReadOnlyDto>>> GetAuthors()
        {
            Response<List<AuthorReadOnlyDto>> response;
            try
            {
                await GetBearerTokenAsync();
                var authors = await client.AuthorsAllAsync();
                response = new Response<List<AuthorReadOnlyDto>>
                { 
                    Data = authors.ToList(),
                    Success = true
                };
    
            }
            catch (ApiException ex)
            {

                response = ConvertApiExceptions<List<AuthorReadOnlyDto>>(ex);
            }
            return response;            
        }

        
        public async Task<Response<int>> Create(AuthorCreateDto authorCreateDto)
        {
            Response<int> response;
            try
            {
                await GetBearerTokenAsync();
                var author = await client.AuthorsPOSTAsync(authorCreateDto);
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
                await client.AuthorsDELETEAsync(id);
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

        public async Task<Response<int>> Update(int id, AuthorUpdateDto author)
        {
            Response<int> response = new();
            try
            {
                await GetBearerTokenAsync();
                await client.AuthorsPUTAsync(id, author);
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
        public async Task<Response<AuthorDetailsDto>> GetAuthor(int id)
        {
            Response<AuthorDetailsDto> response;
            try
            {
                await GetBearerTokenAsync();
                var author = await client.AuthorsGETAsync(id);
                response = new Response<AuthorDetailsDto>
                {
                    Data = author,
                    Success = true
                };

            }
            catch (ApiException ex)
            {

                response = ConvertApiExceptions<AuthorDetailsDto>(ex);
            }
            return response;
        }

        public async Task<Response<AuthorUpdateDto>> GetForUpdate(int id)
        {
            Response<AuthorUpdateDto> response;
            try
            {
                await GetBearerTokenAsync();
                var author = await client.AuthorsGETAsync(id);
                response = new Response<AuthorUpdateDto>
                {
                    Data = mapper.Map<AuthorUpdateDto>(author),
                    Success = true
                };
            }
            catch (ApiException ex)
            {

                response = ConvertApiExceptions<AuthorUpdateDto>(ex);
            }
            return response;
        }
    }
}
