using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.ViewModels
{
    public class AddCategoryViewModel
    {
        [Required]
        public string CategoryName { get; set; }
        
        [Required]
        public string Description { get; set; }
    }
}