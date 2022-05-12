using System;
using System.Collections.Generic;

namespace Reservation_vols_version2_withEF.Models
{
    public partial class Client
    {
        public Client()
        {
            TicketClients = new HashSet<Ticket>();
            TicketPassengers = new HashSet<Ticket>();
        }

        public int Clientid { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime Birthdate { get; set; }
        public string Phonenumber { get; set; } = null!;

        public virtual ICollection<Ticket> TicketClients { get; set; }
        public virtual ICollection<Ticket> TicketPassengers { get; set; }
    }
}
