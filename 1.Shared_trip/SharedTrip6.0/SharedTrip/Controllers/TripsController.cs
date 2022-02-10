using BasicWebServer.Server.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Server.Controllers
{
    public class TripsController : Controller
    {
        public TripsController(Request request) 
            : base(request)
        {
        }
    }
}
