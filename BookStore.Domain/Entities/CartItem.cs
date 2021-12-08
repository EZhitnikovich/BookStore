namespace BookStore.Domain.Entities
{
    public class CartItem: BaseEntity
    {
        public Book Book { get; set; }
        public int Amount { get; set; }
        public string ShopCartId { get; set; }
    }
}