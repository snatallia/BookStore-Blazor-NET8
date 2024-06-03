using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Providers;
using BookStoreApp.Blazor.Server.UI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;
using System;

namespace BookStoreApp.Blazor.Server.UI.Services.Authentication
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        private readonly IClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthenticationService(IClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
            : base(httpClient, localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<Response<AuthResponse>> AuthenticateAsync(LoginUserDto loginModel)
        {
            Response<AuthResponse> response;
            try
            {
                var result = await httpClient.LoginAsync(loginModel);
                response = new Response<AuthResponse>
                {
                    Data = result,
                    Success = true,
                };

                await localStorage.SetItemAsync("accessToken", result.Token);

                //change auth state
                await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedIn();

          
            }
            catch (ApiException ex)
            {

                response = ConvertApiExceptions<AuthResponse>(ex);
            }

            return response;
        }

        public async Task<bool> LogoutAsync()
        {
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedOut();
            return true;
        }
    }
}