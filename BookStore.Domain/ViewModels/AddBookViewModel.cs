using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Domain.ViewModels
{
    public class AddBookViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Название книги должно содержать от 2 до 50 символов")]
        public string BookName { get; set; }
        
        [Required]
        [StringLength(250, MinimumLength = 25, ErrorMessage = "Описание книги должно содержать от 25 до 250 символов")]
        public string Description { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }
        
        [Required]
        [DataType(DataType.Url)]
        public string Image { get; set; }
        
        [Required]
        [Range(0.01, float.MaxValue, ErrorMessage = "Цена должна принимать значение больше нуля")]
        public float Price { get; set; }
        
        [Required]
        public int? CategoryId { get; set; }
    }
}