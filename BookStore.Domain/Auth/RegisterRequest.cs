using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Auth
{
    public class RegisterRequest
    {
        [Required]
        [Display(Name = "Имя")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Имя должно содержать от 2 до 25 символов")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Фамилия должна содержать от 2 до 25 символов")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Некорректный формат почты")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Пароль должен содержать от 8 до 20 символов")]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}