using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.HTTP;
using SharedTrip.Contracts;
using SharedTrip.Models;
using System;
using System.Collections.Generic;

namespace BasicWebServer.Server.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripService tripService;
        public TripsController(Request request,ITripService _tripService)
            : base(request)
        {
             tripService = _tripService;    
        }

        [Authorize]
        public Response Add() => View();

        [Authorize]
        [HttpPost]
        public Response Add(TripViewModel model)
        {
            var (IsValid, errors) = tripService.ValidateModel(model);

            if (!IsValid)
            {
                return View(errors, "/Error");
            }

            try
            {
                tripService.AddTrip(model);
            }
            catch (Exception)
            {
                return View(new List<ErrorViewModel> { new ErrorViewModel("Unexpected error") }, "Error");
            }

            return Redirect("/");
        }
    }
}
