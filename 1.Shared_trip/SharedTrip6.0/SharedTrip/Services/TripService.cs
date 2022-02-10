using SharedTrip.Contracts;
using SharedTrip.Data.Common;
using SharedTrip.Data.Models;
using SharedTrip.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedTrip.Services
{
    public class TripService : ITripService
    {
        private readonly IRepository repo;

        public TripService(IRepository _repo)
        {
            repo = _repo;
        }

        public void AddTrip(TripViewModel model)
        {
            Trip trip = new Trip()
            {
                DepartureTime = model.ConvertedDate,
                Description = model.Description,
                EndPoint = model.EndPoint,
                StartPoint = model.StartPoint,
                Seats = model.Seats,
                ImagePath = model.ImagePath

            };

            repo.Add(trip);
            repo.SaveChanges(); 

        }

        public (bool isValid, IEnumerable<ErrorViewModel> errors) ValidateModel(TripViewModel model)
        {
            bool isValid = true;
            List<ErrorViewModel> errors = new List<ErrorViewModel>();
            DateTime date;

            if (string.IsNullOrWhiteSpace(model.StartPoint))
            {
                isValid = false;
                errors.Add(new ErrorViewModel("StartPoint is required"));
            }

            if (string.IsNullOrWhiteSpace(model.EndPoint))
            {
                isValid = false;
                errors.Add(new ErrorViewModel("EndPoint is required"));
            }

            if (string.IsNullOrWhiteSpace(model.Description) || model.Description.Length > 80)
            {
                isValid = false;
                errors.Add(new ErrorViewModel("Description is required and must not be more than 80 characters"));
            }

            if (model.Seats < 2 || model.Seats > 6)
            {
                isValid = false;
                errors.Add(new ErrorViewModel("Seats must be between 2 and 6"));
            }

            if (!DateTime.TryParseExact(
                model.DepartureTime,
                "dd.MM.yyyy HH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out date))
            {
                isValid = false;
                errors.Add(new ErrorViewModel("Seats must be between 2 and 6"));
            }
            else
            {
                model.ConvertedDate = date;
            }


            return (isValid, errors);
        }
    }
}
