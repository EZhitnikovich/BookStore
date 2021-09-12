using System.Collections.Generic;
using BookStore.Domain;

namespace BookStore.Data.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Book> Books { get; set; }
    }
}