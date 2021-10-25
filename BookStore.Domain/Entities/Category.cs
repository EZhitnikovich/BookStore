using System.Collections.Generic;

namespace BookStore.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public List<Book> Books { get; set; }
    }
}