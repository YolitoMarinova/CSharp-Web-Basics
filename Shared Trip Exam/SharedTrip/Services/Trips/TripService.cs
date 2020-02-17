using SharedTrip.Models;
using SharedTrip.ViewModels;
using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SharedTrip.Services.Trips
{
    public class TripService : ITripsService
    {
        private readonly ApplicationDbContext db;

        public TripService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateTrip(string startPoint, string endPoint, DateTime departureTime, string imagePath, int seats, string description)
        {
            var trip = new Trip
            {
                StartPoint = startPoint,
                EndPoint = endPoint,
                DepartureTime = departureTime,
                ImagePath = imagePath,
                Seats = seats,
                Description = description
            };

            this.db.Trips.Add(trip);
            this.db.SaveChanges();
        }

        public AllTripsViewModel GettAllTrips()
        {
            var viewModel = new AllTripsViewModel();

            viewModel.Trips = this.db.Trips.Select(t => new TripViewModel
            {
                Id = t.Id,
                StartPoint = t.StartPoint,
                EndPoint = t.EndPoint,
                DepartureTime = t.DepartureTime,
                Seats = t.Seats - t.Users.Count()
            })
                .ToList();

            return viewModel;
        }

        public bool IsTripExist(string tripId)
        {
            return this.db.Trips.Any(t => t.Id == tripId);
        }

        public DetailsTripViewModel GetTripDetails(string tripId)
        {
            var trip = this.db.Trips.Where(t => t.Id == tripId).Select(t => new DetailsTripViewModel
            {
                Id = t.Id,
                StartPoint = t.StartPoint,
                EndPoint = t.EndPoint,
                DepartureTime = t.DepartureTime,
                Seats = t.Seats - t.Users.Count(),
                ImagePath = t.ImagePath,
                Description = t.Description
            })
                .FirstOrDefault();

            return trip;
        }

        public void JoinUserToTrip(string userId, string tripId)
        {
            var userTrip = new UserTrips { UserId = userId, TripId = tripId };

            this.db.UsersTrips.Add(userTrip);
            this.db.SaveChanges();
        }

        public bool IsUserAlreadyJoinedToATrip(string userId, string tripId)
        {
            return this.db.Trips.Where(t => t.Id == tripId).Any(t => t.Users.Any(u => u.UserId == userId));
        }
    }
}
