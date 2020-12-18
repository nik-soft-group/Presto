using System.ComponentModel.DataAnnotations;

namespace NiksoftCore.ViewModel.User
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is required - نام کاربری نمی تواند خالی باشد")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
