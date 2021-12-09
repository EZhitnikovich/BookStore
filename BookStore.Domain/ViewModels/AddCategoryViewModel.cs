using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.ViewModels
{
    public class AddCategoryViewModel
    {
        [Required]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Название категории должно содержать от 2 до 25 символов")]
        public string CategoryName { get; set; }
        
        [Required]
        [StringLength(250, MinimumLength = 25, ErrorMessage = "Описание категории должно содержать от 25 до 250 символов")]
        public string Description { get; set; }
    }
}