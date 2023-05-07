﻿using System.ComponentModel.DataAnnotations;

namespace MyAds.Models
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "Username is required !")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required !")]
        public string Password { get; set; }
    }
}