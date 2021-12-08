using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Domain.Entities;

namespace BookStore.Service
{
    public interface ISessionCartService
    {
        public string GetCartId();
        public void AddToCart(Book book);
        public int RemoveFromCart(Book book);
        public List<CartItem> GetCartItems();
        public void ClearCart();
        public float GetSopCartTotal();
    }
}