using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Cart
{
    public class ShoppingCart
    {
        private readonly AppDbContext _context;
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }
        public void AddItemToCart(Movie movie)
        {
            if(_context.ShoppingCartItems.Where(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId).ToList().Count == 0)
            {
                _context.ShoppingCartItems.Add(
                    new ShoppingCartItem()
                    {
                        Movie = movie,
                        Amount = 1,
                        ShoppingCartId = ShoppingCartId
                    }
               );
            } else
            {
                var shoppingCart = _context.ShoppingCartItems.FirstOrDefault(n => n.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);
                shoppingCart.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCart(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem != null)
            {
                if(shoppingCartItem.Amount>1)
                  shoppingCartItem.Amount--;
                else 
                   _context.ShoppingCartItems.Remove(shoppingCartItem);
            }
            _context.SaveChanges();
        }
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems
                   .Where(n => n.ShoppingCartId == ShoppingCartId)
                   .Include(n => n.Movie).ToList());
        }
        public double GetShoppingCartTotal() => _context.ShoppingCartItems
                .Where(n => n.ShoppingCartId == ShoppingCartId)
                .Select(n => n.Movie.Price * n.Amount).Sum();

    }
}
