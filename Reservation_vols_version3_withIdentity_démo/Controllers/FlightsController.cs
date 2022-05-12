#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reservation_vols_version2_withEF.Models;
using Reservation_vols_version2_withEF.ViewModel;
using Reservation_vols_version3_withIdentity_démo.Models;

namespace Reservation_vols_version2_withEF.Controllers
{
    public class FlightsController : Controller
    {
        private readonly Reservation_volsContext _context;

        public FlightsController(Reservation_volsContext context)
        {
            _context = context;
        }

        // GET: Flights
        public async Task<IActionResult> Index(string? DepartureAirport, string? ArrivalAirport)
        {
            //On préparer la requète, on va chercher uniquement les nom (select air.Name) pour les aéroports qui n'ont pas été supprimés (air.Isdeleted==false)
            var airports = from air in _context.Airports where air.Isdeleted==false select air.Name;

            IQueryable<Flight> reservation_volsContext = _context.Flights.Include(f => f.AirportArrival)
                    .Include(f => f.AirportDeparture);

            if (!string.IsNullOrEmpty(DepartureAirport))
            {
                reservation_volsContext = reservation_volsContext.Where(x => (string)x.AirportDeparture.Name == DepartureAirport);
            }

            if (!string.IsNullOrEmpty(ArrivalAirport))
            {
                reservation_volsContext = reservation_volsContext.Where(x => (string)x.AirportArrival.Name == ArrivalAirport);
            }


            var filteredIndexFlightsVM = new FilteredIndexFlightsVM
            {
                Airports = new SelectList(await airports.ToListAsync()),
                Flights = await reservation_volsContext.ToListAsync()
            };

            return View(filteredIndexFlightsVM);
        }

        // GET: Flights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .Include(f => f.AirportArrival)
                .Include(f => f.AirportDeparture)
                .FirstOrDefaultAsync(m => m.Flightid == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // GET: Flights/Create
        public IActionResult Create()
        {
            ViewData["AirportArrivalId"] = new SelectList(_context.Airports, "Airportid", "Name");
            ViewData["AirportDepartureId"] = new SelectList(_context.Airports, "Airportid", "Name");
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DateDeparture,DateArrival,AirportDepartureId,AirportArrivalId")] CreatedFlightVM createdFlight)
        {
            Flight flight = new Flight(createdFlight.DateDeparture,createdFlight.DateArrival,createdFlight.AirportDepartureId, createdFlight.AirportArrivalId);
            flight.AirportArrival = await _context.Airports.FindAsync(createdFlight.AirportArrivalId);
            flight.AirportDeparture = await _context.Airports.FindAsync(createdFlight.AirportDepartureId);

            if (ModelState.IsValid)
            {
                _context.Add(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AirportArrivalId"] = new SelectList(_context.Airports, "Airportid", "Name", flight.AirportArrivalId);
            ViewData["AirportDepartureId"] = new SelectList(_context.Airports, "Airportid", "Name", flight.AirportDepartureId);
            return View(flight);
        }

        // GET: Flights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            ViewData["AirportArrivalId"] = new SelectList(_context.Airports, "Airportid", "Airportid", flight.AirportArrivalId);
            ViewData["AirportDepartureId"] = new SelectList(_context.Airports, "Airportid", "Airportid", flight.AirportDepartureId);
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Flightid,Isopen,DateDeparture,DateArrival,AirportDepartureId,AirportArrivalId")] Flight flight)
        {
            if (id != flight.Flightid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.Flightid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AirportArrivalId"] = new SelectList(_context.Airports, "Airportid", "Airportid", flight.AirportArrivalId);
            ViewData["AirportDepartureId"] = new SelectList(_context.Airports, "Airportid", "Airportid", flight.AirportDepartureId);
            return View(flight);
        }

        // GET: Flights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .Include(f => f.AirportArrival)
                .Include(f => f.AirportDeparture)
                .FirstOrDefaultAsync(m => m.Flightid == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(int id)
        {
            return _context.Flights.Any(e => e.Flightid == id);
        }
    }
}
