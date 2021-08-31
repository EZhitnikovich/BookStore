using System.Collections.Generic;
using BookStore.Data.Entities;

namespace BookStore.Data.Models
{
    public class Category: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Book> Books { get; set; }
    }
}