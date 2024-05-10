﻿using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Models.User
{
    public class UserDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required] 
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}