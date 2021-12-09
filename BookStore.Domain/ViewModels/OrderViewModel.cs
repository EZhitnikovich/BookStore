using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookStore.Domain.Entities;

namespace BookStore.Domain.ViewModels
{
    public class OrderViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Information { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}