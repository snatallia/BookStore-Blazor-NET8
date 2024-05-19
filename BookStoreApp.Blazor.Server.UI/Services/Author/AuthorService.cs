using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Services.Base;
using System.Collections.Generic;

namespace BookStoreApp.Blazor.Server.UI.Services.Author
{
    public class AuthorService: BaseHttpService, IAuthorService
    {
        private readonly IClient client;

        public AuthorService(IClient client, ILocalStorageService localStorage): base(client, localStorage)
        {
            this.client = client;
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
    }
}
