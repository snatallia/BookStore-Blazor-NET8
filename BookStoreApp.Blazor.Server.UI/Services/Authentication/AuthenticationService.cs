using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Providers;
using BookStoreApp.Blazor.Server.UI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStoreApp.Blazor.Server.UI.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthenticationService(IClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> AuthenticateAsync(LoginUserDto loginModel)
        {
            var response = await httpClient.LoginAsync(loginModel);
            //store token in local storage
            await localStorage.SetItemAsync("accessToken", response.Token);

            //change auth state
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedIn();
            return true;
        }

        public async Task<bool> LogoutAsync()
        {
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedOut();
            return true;
        }
    }
}