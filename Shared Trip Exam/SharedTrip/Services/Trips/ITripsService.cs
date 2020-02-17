using SharedTrip.ViewModels;
using SharedTrip.ViewModels.Trips;
using System;

namespace SharedTrip.Services.Trips
{
    public interface ITripsService
    {
        void CreateTrip(string startPoint, string endPoint, DateTime departureTime, string imagePath, int seats, string description);

        AllTripsViewModel GettAllTrips();

        DetailsTripViewModel GetTripDetails(string tripId);

        bool IsTripExist(string tripId);

        void JoinUserToTrip(string userId, string tripId);

        bool IsUserAlreadyJoinedToATrip(string userId, string tripId);
    }
}
