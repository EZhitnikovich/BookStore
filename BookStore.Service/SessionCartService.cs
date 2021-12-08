using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Domain.Entities;
using BookStore.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BookStore.Service
{
    public class SessionCartService: ISessionCartService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartKey = "CartId";

        public SessionCartService(ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCartId()
        {
            var session = _httpContextAccessor.HttpContext.Session;

            var cartId = session.GetString(CartKey) ?? Guid.NewGuid().ToString();
            
            session.SetString(CartKey, cartId);

            return cartId;
        }

        public void AddToCart(Book book)
        {
            var cartId = GetCartId();
            var shopCartItem = _context.CartItems.SingleOrDefault(
                s => s.Book.Id == book.Id && s.ShopCartId == cartId);

            if (shopCartItem == null)
            {
                shopCartItem = new CartItem()
                {
                    ShopCartId = cartId,
                    Book = book,
                    Amount = 1
                };

                _context.CartItems.Add(shopCartItem);
            }
            else
            {
                shopCartItem.Amount++;
            }

            _context.SaveChanges();
        }

        public int RemoveFromCart(Book book)
        {
            var cartId = GetCartId();
            var shopCartItem = _context.CartItems.SingleOrDefault(
                s => s.Book.Id == book.Id && s.ShopCartId == cartId);

            var localAmount = 0;

            if (shopCartItem != null)
            {
                if (shopCartItem.Amount > 1)
                {
                    shopCartItem.Amount--;
                    localAmount = shopCartItem.Amount;
                }
                else
                {
                    _context.CartItems.Remove(shopCartItem);
                }
            }

            _context.SaveChanges();
            return localAmount;
        }

        public List<CartItem> GetCartItems()
        {
            var cartId = GetCartId();
            return _context.CartItems.Where(c => c.ShopCartId == cartId)
                .Include(s => s.Book).ToList();
        }

        public void ClearCart()
        {
            var cartId = GetCartId();
            var cartItems = _context.CartItems.Where(x => x.ShopCartId == cartId);
            
            _context.CartItems.RemoveRange(cartItems);

            _context.SaveChanges();
        }

        public float GetSopCartTotal()
        {
            var cartId = GetCartId();
            var total = _context.CartItems.Where(x => x.ShopCartId == cartId)
                .Select(c => c.Book.Price * c.Amount).Sum();
            return total;
        }
    }
}