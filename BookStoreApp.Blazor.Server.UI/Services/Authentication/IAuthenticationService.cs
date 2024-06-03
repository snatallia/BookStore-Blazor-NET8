using BookStoreApp.Blazor.Server.UI.Services.Base;
using System.Threading.Tasks;

namespace BookStoreApp.Blazor.Server.UI.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<Response<AuthResponse>> AuthenticateAsync(LoginUserDto loginModel);

        Task<bool> LogoutAsync();
    }
}