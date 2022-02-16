using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using SMS.Contracts;
using SMS.Models;

namespace SMS.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        public UsersController(Request request, IUserService _userService)
             : base(request)
        {
            userService = _userService;
        }

        public Response Login()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/");
            }

            return View(new { IsAuthenticated = false });
        }

        [HttpPost]
        public Response Login(LoginViewModel model)
        {
           string Id = userService.Login(model);

            if (Id == null)
            {
                return View(new { ErrorMessage = "Incorrect Login" }, "/Error");
            }

            Request.Session.Clear();

            SignIn(Id);

            CookieCollection cookies = new CookieCollection();
            cookies.Add(Session.SessionCookieName,
                Request.Session.Id);

            return Redirect("/");

        }

        public Response Register()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/");
            }

            return View(new { IsAuthenticated = false });
        }

        [HttpPost]
        public Response Register(RegisterViewModel model)
        {
            var (isRegistered, error) = userService.Register(model);

            if (isRegistered)
            {
                return Redirect("/Users/Login");
            }

            return View(new  { ErrorMessage = error}, "/Error");
        }
    }
}
