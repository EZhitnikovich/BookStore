using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Auth
{
    public class ResetPasswordRequest
    {
        [Required] [EmailAddress] public string Email { get; set; }

        [Required] public string Token { get; set; }

        [Required] [MinLength(6)] public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}