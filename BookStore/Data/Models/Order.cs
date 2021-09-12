using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookStore.Domain;

namespace BookStore.Data.Models
{
    public class Order: BaseEntity
    {
        public string Address { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool IsAvailable { get; set; }
        public User User { get; set; }
        public List<Book> Books { get; set; }
    }
}