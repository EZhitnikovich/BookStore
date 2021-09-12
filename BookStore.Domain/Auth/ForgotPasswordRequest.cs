using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Auth
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}