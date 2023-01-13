using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMoviesService _service;
        private readonly ShoppingCart _shoppingCart;
        public OrdersController(IMoviesService service, ShoppingCart shoppingCart)
        {
            _service = service;
            _shoppingCart = shoppingCart;   
        }
        public IActionResult Index()
        {
            var allItems = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = allItems.;
            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal(),
            };
            return View(response);
        }
    }
}
