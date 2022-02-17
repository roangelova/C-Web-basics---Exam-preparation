using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using SMS.Contracts;
using SMS.Models;

namespace SMS.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService productService;

        public ProductsController(Request request, IProductService _productService)
            : base(request)
        {
            productService = _productService;
        }

        [Authorize]
        public Response Create()
        {

            return View(new { IsAuthenticated = true });
        }

        [HttpPost]
        public Response Create(CreateViewModel model)
        {
            (bool isCreated, string error) = productService.Create(model);

            if (!isCreated)
            {
                return View(new { ErrorMessage = error }, "/Error");
            } 

            return Redirect("/");

        }
    }
}
