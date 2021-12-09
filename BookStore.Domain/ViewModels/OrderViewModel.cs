using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookStore.Domain.Entities;

namespace BookStore.Domain.ViewModels
{
    public class OrderViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Адрес должен содержать от 2 до 50 символов")]
        public string Address { get; set; }
        
        [StringLength(250, ErrorMessage = "")]
        public string Information { get; set; }
        
        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Мобильный телефон должен иметь формат +XХХХХХХХХ")]
        public string PhoneNumber { get; set; }
    }
}