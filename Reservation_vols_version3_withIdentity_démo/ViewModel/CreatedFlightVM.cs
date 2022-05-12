using System.ComponentModel.DataAnnotations;

namespace Reservation_vols_version2_withEF.ViewModel
{
    public class CreatedFlightVM
    {
        public CreatedFlightVM()
        {

        }

        [Required]
        public DateTime DateDeparture { get; set; }

        [Required]
        public DateTime DateArrival { get; set; }

        [Required]
        public int AirportDepartureId { get; set; }

        [Required]
        public int AirportArrivalId { get; set; }
    }
}
