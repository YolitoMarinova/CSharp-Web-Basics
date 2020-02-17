using SharedTrip.Services.Trips;
using SharedTrip.ViewModels;
using SharedTrip.ViewModels.Trips;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripsService tripsService;

        public TripsController(ITripsService tripsService)
        {
            this.tripsService = tripsService;
        }

        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(CreateTripInputViewModel model)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrWhiteSpace(model.StartPoint))
            {
                return this.Error("Start point is required!");
            }

            if (string.IsNullOrWhiteSpace(model.EndPoint))
            {
                return this.Error("Destination is required!");
            }

            if (model.Seats < 2 || model.Seats > 6)
            {
                return this.Error("Number of seats should be between 2 and 6!");
            }

            if (string.IsNullOrWhiteSpace(model.Description) || model.Description.Length > 80)
            {
                return this.Error("Description should be between 1 and 80 symbols!");
            }

            this.tripsService.CreateTrip(model.StartPoint, model.EndPoint, model.DepartureTime, model.ImagePath, model.Seats, model.Description);

            return this.Redirect("/");
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var allTripsViewModel = this.tripsService.GettAllTrips();

            return this.View(allTripsViewModel);
        }

        public HttpResponse Details(string tripId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (!this.tripsService.IsTripExist(tripId))
            {
                return this.Redirect("/");
            }

            DetailsTripViewModel model = this.tripsService.GetTripDetails(tripId);

            return this.View(model);
        }


        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (this.tripsService.IsUserAlreadyJoinedToATrip(this.User, tripId))
            {
                return this.Redirect("Details?tripId=" + tripId);
            }

            this.tripsService.JoinUserToTrip(this.User, tripId);

            return this.Redirect("/");
        }
    }
}
