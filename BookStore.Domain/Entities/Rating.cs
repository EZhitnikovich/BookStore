using System.ComponentModel.DataAnnotations;
using BookStore.Domain.Auth;

namespace BookStore.Domain.Entities
{
    public class Rating: BaseEntity
    {
        [Range(0, 5)]
        public double Mark { get; set; }
        
        public int? ApplicationUserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int? BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}