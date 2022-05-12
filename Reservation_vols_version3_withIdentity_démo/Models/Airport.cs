using System;
using System.Collections.Generic;

namespace Reservation_vols_version2_withEF.Models
{
    public partial class Airport
    {
        public Airport()
        {
            FlightAirportArrivals = new HashSet<Flight>();
            FlightAirportDepartures = new HashSet<Flight>();
        }

        public int Airportid { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public bool? Isdeleted { get; set; }

        public virtual ICollection<Flight> FlightAirportArrivals { get; set; }
        public virtual ICollection<Flight> FlightAirportDepartures { get; set; }
    }
}
