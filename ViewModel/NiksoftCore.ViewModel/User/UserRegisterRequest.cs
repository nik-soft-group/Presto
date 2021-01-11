﻿using System.ComponentModel.DataAnnotations;

namespace NiksoftCore.ViewModel.User
{
    public class UserRegisterRequest
    {
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن یکسان نیست")]
        public string ConfirmPassword { get; set; }

        public string PhoneNumber { get; set; }
    }
}
