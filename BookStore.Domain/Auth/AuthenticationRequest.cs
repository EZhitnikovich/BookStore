using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Auth
{
    public class AuthenticationRequest
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Неверная почта или пароль")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password, ErrorMessage = "Неверная почта или пароль")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}