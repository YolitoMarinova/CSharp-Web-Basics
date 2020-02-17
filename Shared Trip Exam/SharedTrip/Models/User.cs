using SIS.MvcFramework;
using System;
using System.Collections.Generic;

namespace SharedTrip.Models
{
    public class User : IdentityUser<string>
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Trips = new HashSet<UserTrips>();
        }

        public IEnumerable<UserTrips> Trips { get; set; }
    }
}
