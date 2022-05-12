using System;
using System.Collections.Generic;

namespace Reservation_vols_version2_withEF.Models
{
    public partial class Ticket
    {
        public int Ticketid { get; set; }
        public bool? Isconfirmed { get; set; }
        public int FlightId { get; set; }
        public int ClientId { get; set; }
        public int PassengerId { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual Flight Flight { get; set; } = null!;
        public virtual Client Passenger { get; set; } = null!;
    }
}
