using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.HTTP;
using SharedTrip.Contracts;
using SharedTrip.Models;
using SharedTrip.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Server.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(Request request, IUserService _userService)
             : base(request)
        {
            this.userService = _userService;

        }

        public Response Login()
        => View();

        public Response Register()
        => View();

        [HttpPost]
        public Response Register(RegisterViewModel model)
        {
            var (IsValid, errors) = userService.ValidateModel(model);

            if (!IsValid)
            {
                return View(errors, "/Error");
            }

            try
            {
                userService.RegisterUser(model);
            }
            catch (ArgumentException aex)
            {
                return View(new List<ErrorViewModel> { new ErrorViewModel(aex.Message) }, "Error");
            }
            catch (Exception)
            {
                return View(new List<ErrorViewModel> { new ErrorViewModel("Unexpected error") }, "Error");
            }

            return Redirect("/Users/Login");  
        }

        [HttpPost]
        public Response Login(LoginViewModel model)
        {
            Request.Session.Clear();

            (string userID, bool isCorrect) = userService.IsLoginCorrect(model);

            if (isCorrect)
            {
                SignIn(userID);

                CookieCollection cookies = new CookieCollection();
                cookies.Add(Session.SessionCookieName,
                    Request.Session.Id);

                return Redirect("/");
            }
            else
            {
                return View(new List<ErrorViewModel> { new ErrorViewModel("Login incorrect") }, "Error");
            }
        }
    }
}
