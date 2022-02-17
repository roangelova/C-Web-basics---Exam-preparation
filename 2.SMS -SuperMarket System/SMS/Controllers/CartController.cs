using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;

namespace SMS.Contracts
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(Request request, ICartService _cartService) 
            : base(request)
        {
            cartService = _cartService;
        }

        [Authorize]
        public Response AddProduct(string productId)
        {
            var products = cartService.AddProduct(productId, User.Id);

            return View(new 
            {
            products = products, 
            IsAuthenticated = true
            }, "Carts/Details");
        }
    }
}
