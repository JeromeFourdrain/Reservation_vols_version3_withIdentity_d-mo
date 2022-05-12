using Microsoft.AspNetCore.Mvc.Rendering;
using Reservation_vols_version2_withEF.Models;

namespace Reservation_vols_version2_withEF.ViewModel
{
    public class FilteredIndexFlightsVM
    {
        public List<Flight>? Flights { get; set; }
        public SelectList? Airports { get; set; }

        public string? DepartureAirport { get; set; }

        public string? ArrivalAirport { get; set; }

    }
}
