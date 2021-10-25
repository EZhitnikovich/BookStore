using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Entities
{
    public class Order : BaseEntity
    {
        [DataType(DataType.EmailAddress)] public string Email { get; set; }

        public string Information { get; set; }

        [DataType(DataType.PhoneNumber)] public string PhoneNumber { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}