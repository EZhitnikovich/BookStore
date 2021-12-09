using System;
using System.Collections.Generic;
using BookStore.Domain.Auth;

namespace BookStore.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string Address { get; set; }
        public string Information { get; set; }
        public string PhoneNumber { get; set; }

        public List<CartItem> CartItems { get; set; }
        public DateTime OrderDate { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}