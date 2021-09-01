using BookStore.Data.Entities;

namespace BookStore.Data.Models
{
    public class Book : BaseEntity
    {
        public string Description { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}