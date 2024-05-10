using BookStore.API.Data;
using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Models.User
{
    public class LoginUserDto: ApiUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
