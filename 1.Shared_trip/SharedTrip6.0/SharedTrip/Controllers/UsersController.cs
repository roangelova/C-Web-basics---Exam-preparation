using BasicWebServer.Server.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Server.Controllers
{
    public class UsersController : Controller
    {
        public UsersController(Request request)
             : base(request)
        {




        }

        public Response Login()
        => View();

        public Response Register()
        => View();
    }
}
