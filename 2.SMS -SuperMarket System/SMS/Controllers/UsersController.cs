using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;

namespace SMS.Controllers
{
    public class UsersController : Controller
    {
        public UsersController(Request request)
             : base(request)
        {
        }

        public Response Login()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/");
            }

            return View(new { IsAuthenticated = false }); 
        }

        public Response Register()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/");
            }

            return View(new { IsAuthenticated = false });
        }
    }
}
