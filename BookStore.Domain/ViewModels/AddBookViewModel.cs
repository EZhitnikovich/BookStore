using System.ComponentModel.DataAnnotations;
using BookStore.Domain.Entities;

namespace BookStore.Domain.ViewModels
{
    public class AddBookViewModel
    {
        [Required]
        public string BookName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}