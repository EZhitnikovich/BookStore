using System;

namespace BookStore.Data.Entities
{
    public class BaseEntity: IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}