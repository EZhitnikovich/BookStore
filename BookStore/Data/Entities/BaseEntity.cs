using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Data.Entities
{
    public class BaseEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}