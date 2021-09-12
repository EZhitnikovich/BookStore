namespace BookStore.Domain.Entities
{
    public class OrderDetail: BaseEntity
    {
        public int BookId { get; set; }
        public int OrderId { get; set; }
        public Book Book { get; set; }
        public Order Order { get; set; }
    }
}