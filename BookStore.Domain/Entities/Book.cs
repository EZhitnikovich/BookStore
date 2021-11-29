using System.Collections.Generic;

namespace BookStore.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string BookName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }

        public List<Rating> Marks { get; set; }
        
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}