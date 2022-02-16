using SharedTrip.Models;
using System.Collections.Generic;

namespace SharedTrip.Contracts
{
    public interface ITripService
    {
        (bool isValid, IEnumerable<ErrorViewModel> errors) ValidateModel(TripViewModel model);
        void AddTrip(TripViewModel model);
        IEnumerable<TripListViewModel> GetAllTrips();
        TripDetailsViewModel GetTripDetails(string tripId);
        void AddUserToTrip(string tripId, string id);
    }
}
