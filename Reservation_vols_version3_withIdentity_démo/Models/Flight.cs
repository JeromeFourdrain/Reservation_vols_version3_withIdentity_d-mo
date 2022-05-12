using System;
using System.Collections.Generic;

namespace Reservation_vols_version2_withEF.Models
{
    public partial class Flight
    {
        public Flight()
        {
            Tickets = new HashSet<Ticket>();
        }

        public Flight(DateTime datedeparture, DateTime datearrival, int airportdepartureid, int airportarrivalid)
        {
            this.AirportDepartureId = airportdepartureid;
            this.AirportArrivalId = airportarrivalid;
            this.DateDeparture = datedeparture;
            this.DateArrival = datearrival;
        }
        public int Flightid { get; set; }
        public bool? Isopen { get; set; }
        public DateTime DateDeparture { get; set; }
        public DateTime DateArrival { get; set; }
        public int AirportDepartureId { get; set; }
        public int AirportArrivalId { get; set; }

        public virtual Airport AirportArrival { get; set; } = null!;
        public virtual Airport AirportDeparture { get; set; } = null!;
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
